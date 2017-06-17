using System;
using System.Collections.Generic;
using System.Linq;
using LangParser.AST;
using LangParser.Lexing;
using LangParser.Lexing.Tokens;

namespace LangParser.Parsing
{
	public class Parser
	{
		private TokenStream _tokenStream;

		public Parser(Lexer lexer)
		{
			_tokenStream = new TokenStream(lexer);
		}

		public IEnumerable<Ast> Parse() => TranslationUnit();

		#region Translation units
		private IEnumerable<Ast> TranslationUnit()
		{
			FunctionDefinition def;
			while((def = FunctionDefinition() as FunctionDefinition) != null)
			{
				yield return def;
			}
		}

		private Ast FunctionDefinition()
		{
			VariableDeclaration decl = Declaration() as VariableDeclaration;
			if (decl == null)
				return null;

			_tokenStream.Take<LeftParensToken>();
			List<VariableDeclaration> parameters = DeclarationList().OfType<VariableDeclaration>().ToList();
			_tokenStream.Take<RightParensToken>();

			Scope body = BlockStatement() as Scope;
			if (body == null)
				throw new InvalidSyntaxException("Body of a function must be a block statement", _tokenStream.Current.Position, null);

			return new FunctionDefinition(decl.Name, decl.Type, parameters, body);
		}
		#endregion

		#region Statements
		private Ast Statement()
		{
			return
				_tokenStream.Capture(ExpressionStatement) ??
				_tokenStream.Capture(BlockStatement) ??
				_tokenStream.Capture(IfStatement) ??
				_tokenStream.Capture(LoopStatement) ??
				_tokenStream.Capture(ReturnStatement) ??
				throw new InvalidSyntaxException("Expected statement", _tokenStream.Current.Position, null);
		}

		private Ast ReturnStatement()
		{
			if (!_tokenStream.IsMatch<ReturnToken>())
				return null;

			ReturnToken token = _tokenStream.Take<ReturnToken>();
			Ast expr = new ReturnExpression(token, Expression());

			_tokenStream.Take<SemicolonToken>();
			return expr;
		}

		private Ast ExpressionStatement()
		{
			Ast expr = Expression();
			if (expr == null)
				return null;

			_tokenStream.Take<SemicolonToken>();
			return expr;
		}

		private Ast BlockStatement()
		{
			if (!_tokenStream.IsMatch<LeftBracketToken>())
				return null;

			_tokenStream.Take<LeftBracketToken>();

			List<Ast> statements = new List<Ast>();
			while (!_tokenStream.IsMatch<RightBracketToken>())
				statements.Add(Statement());

			_tokenStream.Take<RightBracketToken>();
			return new Scope(statements);
		}

		private Ast IfStatement()
		{
			if (!_tokenStream.IsMatch<IfToken>())
				return null;

			IfToken token = _tokenStream.Take<IfToken>();
			_tokenStream.Take<LeftParensToken>();

			Ast predicate = Expression();
			if (predicate == null)
				throw new InvalidSyntaxException("Condition of if statement must be an expression", _tokenStream.Current.Position, null);

			_tokenStream.Take<RightParensToken>();

			Ast body = Statement();
			if (body == null)
				throw new InvalidSyntaxException("Body of if statement must be a statement", _tokenStream.Current.Position, null);

			if (!_tokenStream.IsMatch<ElseToken>())
				return new ConditionalBranch(token, predicate, body);

			_tokenStream.Take<ElseToken>();

			Ast alternative = Statement();
			if (alternative == null)
				throw new InvalidSyntaxException("An else statement must be followed by a statement", _tokenStream.Current.Position, null);

			return new ConditionalBranch(token, predicate, body, alternative);
		}

		private Ast LoopStatement()
		{
			return
				_tokenStream.Capture(DoWhileLoop) ??
				_tokenStream.Capture(WhileLoop) ??
				_tokenStream.Capture(ForLoop);
		}

		private Ast DoWhileLoop()
		{
			if (!_tokenStream.IsMatch<DoToken>())
				return null;

			DoToken token = _tokenStream.Take<DoToken>();

			Ast body = Statement();
			if (body == null)
				throw new InvalidSyntaxException("A do-while-loop must contain a body statement", _tokenStream.Current.Position, null);

			_tokenStream.Take<WhileToken>();
			_tokenStream.Take<LeftParensToken>();

			Ast condition = Expression();
			if (condition == null)
				throw new InvalidSyntaxException("Condition of do-while-loop must be an expression", _tokenStream.Current.Position, null);

			_tokenStream.Take<RightParensToken>();
			_tokenStream.Take<SemicolonToken>();

			return new DoWhileLoop(token, condition, body.ToScope());
		}

		private Ast WhileLoop()
		{
			if (!_tokenStream.IsMatch<WhileToken>())
				return null;

			WhileToken token = _tokenStream.Take<WhileToken>();
			_tokenStream.Take<LeftParensToken>();

			Ast condition = Expression();
			if (condition == null)
				throw new InvalidSyntaxException("Condition of while-loop must be an expression", _tokenStream.Current.Position, null);

			_tokenStream.Take<RightParensToken>();

			Ast body = Statement();
			if (body == null)
				throw new InvalidSyntaxException("While-loop must have a body statement", _tokenStream.Current.Position, null);

			return new WhileLoop(token, condition, body.ToScope());
		}

		private Ast ForLoop()
		{
			if (!_tokenStream.IsMatch<ForToken>())
				return null;

			ForToken token = _tokenStream.Take<ForToken>();
			_tokenStream.Take<LeftParensToken>();

			Ast initialization = Expression();
			_tokenStream.Take<SemicolonToken>();
			Ast condition = Expression();
			_tokenStream.Take<SemicolonToken>();
			Ast afterthought = Expression();

			_tokenStream.Take<RightParensToken>();

			Ast body = Statement();
			if (body == null)
				throw new InvalidSyntaxException("For-loop must have a body statement", _tokenStream.Current.Position, null);

			return new ForLoop(token, initialization, condition, afterthought, body.ToScope());
		}
		#endregion

		#region Declarations
		private Ast Declaration()
		{
			if (!IsType(_tokenStream.Current))
				return null;

			AST.Type type = new AST.Type(_tokenStream.Consume());
			IdentifierToken ident = _tokenStream.Take<IdentifierToken>();

			return new VariableDeclaration(ident, type);
		}

		private IEnumerable<Ast> DeclarationList()
		{
			Ast first = Declaration();
			if (first == null)
				yield break;

			yield return first;

			while(_tokenStream.IsMatch<CommaToken>())
			{
				_tokenStream.Take<CommaToken>();

				yield return Declaration()
					?? throw new InvalidSyntaxException($"Expected variable declaration, got {_tokenStream.Current}", _tokenStream.Current.Position, null);
			}
		}
		#endregion

		#region Expressions
		private Ast Expression() => AssignmentExpression();

		private IEnumerable<Ast> ExpressionList()
		{
			Ast first = Expression();
			if (first == null)
				yield break;

			yield return first;

			while(_tokenStream.IsMatch<CommaToken>())
			{
				_tokenStream.Take<CommaToken>();

				yield return Expression()
					?? throw new InvalidSyntaxException($"Expected expression list, got {_tokenStream.Current}", _tokenStream.Current.Position, null);
			}
		}

		private Ast PrimaryExpression()
		{
			switch(_tokenStream.Current)
			{
				case IdentifierToken ident:
					{
						_tokenStream.Take<IdentifierToken>();

						if (_tokenStream.IsMatch<LeftParensToken>())
						{
							_tokenStream.Take<LeftParensToken>();
							List<Ast> args = ExpressionList().ToList();
							_tokenStream.Take<RightParensToken>();

							return new FunctionCall(ident, args);
						}

						return new Variable(ident);
					}

				case LeftParensToken leftParens:
					{
						_tokenStream.Take<LeftParensToken>();
						Ast expr = Expression();
						_tokenStream.Take<RightParensToken>();

						return expr;
					}

				case StringToken str:
					return new Literal(_tokenStream.Take<StringToken>());
				case IntNumberToken intNum:
					return new Literal(_tokenStream.Take<IntNumberToken>());
				case FloatNumberToken floatNum:
					return new Literal(_tokenStream.Take<FloatNumberToken>());
				case TrueToken tTrue:
					return new Literal(_tokenStream.Take<TrueToken>());
				case FalseToken tFalse:
					return new Literal(_tokenStream.Take<FalseToken>());

				default:
					return null;
			}
		}

		private Ast UnaryExpression()
		{
			Token sign =
				_tokenStream.TakeIfMatch<NegationToken>() ??
				_tokenStream.TakeIfMatch<MinusToken>() ??
				_tokenStream.TakeIfMatch<PlusToken>();

			Ast primary = PrimaryExpression()
				?? throw new InvalidSyntaxException("Expected primary expression", _tokenStream.Current.Position, null);

			return (sign != null) ? new UnaryExpression(sign, primary) : primary;
		}

		private Ast MultiplicativeExpression() => BinaryExpression(UnaryExpression, TakeMultiplicativeOperator);

		private Ast AdditiveExpression() => BinaryExpression(MultiplicativeExpression, TakeAdditiveOperator);

		private Ast RelationalExpression() => BinaryExpression(AdditiveExpression, TakeRelationalOperator);

		private Ast EqualityExpression() => BinaryExpression(RelationalExpression, TakeEqualityOperator);

		private Ast LogicalXorExpression() => BinaryExpression(EqualityExpression, _tokenStream.Take<XorToken>);

		private Ast LogicalAndExpression() => BinaryExpression(LogicalXorExpression, _tokenStream.Take<AndToken>);

		private Ast LogicalOrExpression() => BinaryExpression(LogicalAndExpression, _tokenStream.Take<OrToken>);

		private Ast AssignmentExpression()
		{
			Func<Ast> declaration = () =>
			{
				if (!IsType(_tokenStream.Current))
					return null;

				Ast decl = Declaration();
				AssignmentToken assignment = _tokenStream.Take<AssignmentToken>();
				Ast expr = Expression();

				return new BinaryExpression(decl, assignment, expr);
			};

			Func<Ast> reAssignment = () =>
			{
				if (!_tokenStream.IsMatch<IdentifierToken>())
					return null;

				Variable var = new Variable(_tokenStream.Take<IdentifierToken>());
				AssignmentToken assignment = _tokenStream.Take<AssignmentToken>();
				Ast expr = Expression();

				return new BinaryExpression(var, assignment, expr);
			};

			return
				_tokenStream.Capture(declaration) ??
				_tokenStream.Capture(reAssignment) ??
				_tokenStream.Capture(LogicalOrExpression);
		}

		private Ast BinaryExpression(Func<Ast> higherPrec, Func<Token> op)
		{
			Ast res = higherPrec();
			try
			{
				while(true)
				{
					Token opToken = op();
					Ast next = higherPrec();
					res = new BinaryExpression(res, opToken, next);
				}
			}
			catch { }

			return res;
		}
		#endregion

		#region Checking Tokens for general types
		private static bool IsType(Token token)
		{
			return
				token is VoidToken ||
				token is ByteToken ||
				token is IntToken ||
				token is FloatToken ||
				token is BoolToken ||
				token is StringKeywordToken ||
				token is ObjectToken;
		}

		private static bool IsUnaryOperator(Token token)
		{
			return
				token is NegationToken ||
				token is PlusToken ||
				token is MinusToken;
		}

		private Token TakeAdditiveOperator()
		{
			return
				_tokenStream.TakeIfMatch<PlusToken>() ??
				_tokenStream.TakeIfMatch<MinusToken>() ??
				throw new InvalidSyntaxException($"Expected additive operator but got {_tokenStream.Current}", _tokenStream.Current.Position, null);
		}

		private Token TakeMultiplicativeOperator()
		{
			return
				_tokenStream.TakeIfMatch<MultiplyToken>() ??
				_tokenStream.TakeIfMatch<DivisionToken>() ??
				throw new InvalidSyntaxException($"Expected multiplicative operator but got {_tokenStream.Current}", _tokenStream.Current.Position, null);
		}

		private Token TakeRelationalOperator()
		{
			return
				_tokenStream.TakeIfMatch<LessThanToken>() ??
				_tokenStream.TakeIfMatch<GreaterThanToken>() ??
				_tokenStream.TakeIfMatch<LessOrEqualToken>() ??
				_tokenStream.TakeIfMatch<GreaterOrEqualToken>() ??
				throw new InvalidSyntaxException($"Expected relational operator but got {_tokenStream.Current}", _tokenStream.Current.Position, null);
		}

		private Token TakeEqualityOperator()
		{
			return
				_tokenStream.TakeIfMatch<EqualsToken>() ??
				_tokenStream.TakeIfMatch<NotEqualToken>() ??
				throw new InvalidSyntaxException($"Expected equality operator but got {_tokenStream.Current}", _tokenStream.Current.Position, null);
		}

		private Token TakeAssignmentOperator()
			=> _tokenStream.TakeIfMatch<AssignmentToken>()
			?? throw new InvalidSyntaxException($"Expected assigment operator but got {_tokenStream.Current}", _tokenStream.Current.Position, null);
		#endregion
	}
}
