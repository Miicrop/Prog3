using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace OOPGames
{
    public class J_TicTacToePaint : J_BaseTicTacToePaint
    {
        public override string Name => "J_Tic-Tac-Toe_Paint";

        public override void PaintTicTacToeField(Canvas canvas, I_J_TicTacToeField currentField)
        {
            Color bgColor = Color.FromRgb(255, 255, 255);
            Color lineColor = Color.FromRgb(255, 0, 0);
            Color XColor = Color.FromRgb(0, 255, 0);
            Color OColor = Color.FromRgb(0, 0, 255);
            
            Brush lineStroke = new SolidColorBrush(lineColor);
            Brush XStroke = new SolidColorBrush(XColor);
            Brush OStroke = new SolidColorBrush(OColor);
            
            Line l1 = new Line() { X1 = 120, Y1 =  20, X2 = 120, Y2 = 320, Stroke = lineStroke, StrokeThickness = 3.0 };
            Line l2 = new Line() { X1 = 220, Y1 =  20, X2 = 220, Y2 = 320, Stroke = lineStroke, StrokeThickness = 3.0 };
            Line l3 = new Line() { X1 =  20, Y1 = 120, X2 = 320, Y2 = 120, Stroke = lineStroke, StrokeThickness = 3.0 };
            Line l4 = new Line() { X1 =  20, Y1 = 220, X2 = 320, Y2 = 220, Stroke = lineStroke, StrokeThickness = 3.0 };

            canvas.Children.Clear();
            canvas.Background = new SolidColorBrush(bgColor);
            canvas.Children.Add(l1);
            canvas.Children.Add(l2);
            canvas.Children.Add(l3);
            canvas.Children.Add(l4);

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (currentField[i, j] == 1)
                    {
                        Line X1 = new Line()
                        {
                            X1 = 20 + (j * 100), 
                            Y1 = 20 + (i * 100), 
                            X2 = 120 + (j * 100),
                            Y2 = 120 + (i * 100), 
                            Stroke = XStroke, StrokeThickness = 3.0
                        };
                        Line X2 = new Line()
                        {
                            X1 =  20 + (j * 100), 
                            Y1 = 120 + (i * 100), 
                            X2 = 120 + (j * 100),
                            Y2 =  20 + (i * 100), 
                            Stroke = XStroke, StrokeThickness = 3.0
                        };
                        canvas.Children.Add(X1);
                        canvas.Children.Add(X2);
                    }
                    else if (currentField[i, j] == 2)
                    {
                        Ellipse OE = new Ellipse()
                        {
                            Margin = new Thickness(
                                20 + (j * 100),
                                20 + (i * 100),
                                0,
                                0),
                            Width = 100, Height = 100, Stroke = OStroke, StrokeThickness = 3.0
                        };
                        canvas.Children.Add(OE);
                    }
                }
            }

        }
    }

    public class J_TicTacToeField : J_BaseTicTacToeField
    {
        int[,] _Field = new int[3, 3] { { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 } };
        
        public override int this[int row, int col]
        {
            get
            {
                if (row >= 0 && row < 3 && col >= 0 && col < 3)
                {
                    return _Field[row, col];
                }
                return -1;
            }
            
            set
            {
                if (row >= 0 && row < 3 && col >= 0 && col < 3)
                {
                    _Field[row, col] = value;
                }
            }
        }
    }

    public class J_TicTacToeRules : J_BaseTicTacToeRules
    {
        public override string Name => "J_Tic-Tac-Toe_Rules";
        
        J_TicTacToeField _Field = new J_TicTacToeField();
        
        public override I_J_TicTacToeField TicTacToeField => _Field;
        
        public override bool MovesPossible
        {
            get
            {
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if (_Field[i, j] == 0)
                        {
                            return true;
                        }
                    }
                }
                return false;
            }
        }
        public override int CheckIfPLayerWon()
        {
            for (int i = 0; i < 3; i++)
            {
                if (_Field[i, 0] > 0 && _Field[i, 0] == _Field[i, 1] && _Field[i, 1] == _Field[i, 2])
                {
                    return _Field[i, 0];
                }
                if (_Field[0, i] > 0 && _Field[0, i] == _Field[1, i] && _Field[1, i] == _Field[2, i])
                {
                    return _Field[0, i];
                }
            }
            
            if (_Field[0, 0] > 0 && _Field[0, 0] == _Field[1, 1] && _Field[1, 1] == _Field[2, 2])
            {
                return _Field[0, 0];
            }
            if (_Field[0, 2] > 0 && _Field[0, 2] == _Field[1, 1] && _Field[1, 1] == _Field[2, 0])
            {
                return _Field[0, 2];
            }

            return -1;
        }

        public override void ClearField()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    _Field[i, j] = 0;
                }
            }
        }

        public override void DoTicTacToeMove(I_J_TicTacToeMove move)
        {
            if (move.Row >= 0 && move.Row < 3 && move.Column >= 0 && move.Column < 3)
            {
                _Field[move.Row, move.Column] = move.PlayerNumber;
            }
        }
    }

    public class J_TicTacToeMove : I_J_TicTacToeMove
    {
        int _Row { get; }
        int _Column { get; }
        int _PlayerNumber { get; }

        public J_TicTacToeMove (int row, int column, int playerNumber)
        {
            _Row = row;
            _Column = column;
            _PlayerNumber = playerNumber;
        }
        public int Row => _Row;
        public int Column => _Column;
        public int PlayerNumber => _PlayerNumber;
    }

    public class J_TicTacToeHumanPlayer : J_BaseHumanTicTacToePlayer
    {
        int _PlayerNumber = 0;
        public override string Name => "J_Tic-Tac-Toe_HumanPlayer";
        public override int PlayerNumber => _PlayerNumber;
        
        public override void SetPlayerNumber(int playerNumber)
        {
            _PlayerNumber = playerNumber;
        }
        
        public override IGamePlayer Clone()
        {
            J_TicTacToeHumanPlayer ttthp = new J_TicTacToeHumanPlayer();
            ttthp.SetPlayerNumber(_PlayerNumber);
            return ttthp;
        }

        public override I_J_TicTacToeMove GetMove(IMoveSelection selection, I_J_TicTacToeField field)
        {
            if (!(selection is IClickSelection sel)) return null;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (sel.XClickPos > 20 + (j * 100) && sel.XClickPos < 120 + (j * 100) &&
                        sel.YClickPos > 20 + (i * 100) && sel.YClickPos < 120 + (i * 100) &&
                        field[i, j] <= 0)
                    {
                        return new J_TicTacToeMove(i,j, _PlayerNumber);
                    }
                }
            }
            return null;
        }
    }

    public class J_TicTacToeComputerPlayer : J_BaseComputerTicTacToePlayer
    {
        int _PlayerNumber = 0;
        public override string Name => "J_Tic-Tac-Toe_ComputerPlayer";
        public override int PlayerNumber => _PlayerNumber;
        
        public override void SetPlayerNumber(int playerNumber)
        {
            _PlayerNumber = playerNumber;
        }

        public override IGamePlayer Clone()
        {
            J_TicTacToeComputerPlayer tttcp = new J_TicTacToeComputerPlayer();
            tttcp.SetPlayerNumber(_PlayerNumber);
            return tttcp;
        }

        public override I_J_TicTacToeMove GetMove(I_J_TicTacToeField field)
        {
            Random rand = new Random();
            int f = rand.Next(0, 8);
            for (int i = 0; i < 9; i++)
            {
                int c = f % 3;
                int r = ((f - c) / 3) % 3;
                if (field[c, r] <= 0)
                {
                    return new J_TicTacToeMove(r, c, _PlayerNumber);
                }
                else
                {
                    f++;
                }
            }
            return null;
        }
    }
}