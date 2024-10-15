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
   public static class GlobalVariables
   {
      public const int ColCount = 7;
      public const int RowCount = 6;
   }
   
   public class J_VierGewinntPaint : IJPaintVierGewinnt
   {
      public string Name => "VierGewinnt Paint";

      public void PaintGameField(Canvas canvas, IGameField currentField)
      {
         if (currentField is IJVierGewinntField)
         {
            PaintVierGewinntField(canvas, (IJVierGewinntField)currentField);
         }
      }

      public void PaintVierGewinntField(Canvas canvas, IJVierGewinntField currentField)
      {
         Color bgColor = Color.FromRgb(255, 255, 255);
         Color lineColor = Color.FromRgb(255, 0, 0);
         Color player1Color = Color.FromRgb(0, 255, 0);
         Color player2Color = Color.FromRgb(0, 0, 255);
            
         Brush lineStroke = new SolidColorBrush(lineColor);
         Brush player1Stroke = new SolidColorBrush(player1Color);
         Brush player2Stroke = new SolidColorBrush(player2Color);

         canvas.Children.Clear();
         canvas.Background = new SolidColorBrush(bgColor);
         
         for (int i = 0; i <= GlobalVariables.RowCount; i++)
         {
            Line l = new Line()
            {
               X1 = 0,
               X2 = GlobalVariables.ColCount * 100,
               Y1 = 0 + i * 100,
               Y2 = 0 + i * 100,
               Stroke = lineStroke,
               StrokeThickness = 3.0
            };
            canvas.Children.Add(l);
         }
         
         for (int i = 0; i <= GlobalVariables.ColCount; i++)
         {
            Line l = new Line()
            {
               X1 = 0 + i * 100,
               X2 = 0 + i * 100,
               Y1 = 0,
               Y2 = GlobalVariables.RowCount * 100,
               Stroke = lineStroke,
               StrokeThickness = 3.0
            };
            canvas.Children.Add(l);
         }
         
         for (int i = 0; i < GlobalVariables.RowCount; i++)
         {
            for (int j = 0; j < GlobalVariables.ColCount; j++)
            {
               if (currentField[i, j] == 1)
               {
                  Ellipse circlePlayer1 = new Ellipse()
                  {
                     Margin = new Thickness(
                        (j * 100),
                        (i * 100),
                        0,
                        0),
                     Width = 100, Height = 100, Stroke = player1Stroke, StrokeThickness = 3.0, Fill=player1Stroke
                  };
                  canvas.Children.Add(circlePlayer1);
               }
               else if (currentField[i, j] == 2)
               {
                  Ellipse circlePlayer2 = new Ellipse()
                  {
                     Margin = new Thickness(
                        (j * 100),
                        (i * 100),
                        0,
                        0),
                     Width = 100, Height = 100, Stroke = player2Stroke, StrokeThickness = 3.0, Fill=player2Stroke
                  };
                  canvas.Children.Add(circlePlayer2);
               }
            }
         }
      }
   }

   public class J_VierGewinntField : IJVierGewinntField
   {
      int[,] _Field = new int[GlobalVariables.RowCount, GlobalVariables.ColCount];

      public bool CanBePaintedBy(IPaintGame painter)
      {
         return painter is IJPaintVierGewinnt;
      }

      public int this[int row, int col]
      {
         get
         {
            if (row >= 0 && row < GlobalVariables.RowCount && col >= 0 && col < GlobalVariables.ColCount)
            {
               return _Field[row, col];
            }

            return -1;
         }

         set
         {
            if (row >= 0 && row < GlobalVariables.RowCount && col >= 0 && col < GlobalVariables.ColCount)
            {
               _Field[row, col] = value;
            }
         }
      }
   }

   public class J_VierGewinntRules : IJVierGewinntRules
   {
      public string Name => "VierGewinnt Rules";
      J_VierGewinntField _Field = new J_VierGewinntField();
      public IJVierGewinntField VierGewinntField => _Field;
      public IGameField CurrentField => VierGewinntField;

      private int _CurrentPlayer = 1;

      public bool MovesPossible
      {
         get
         {
            for (int i = 0; i < GlobalVariables.RowCount; i++)
            {
               for (int j = 0; j < GlobalVariables.ColCount; j++)
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

      public void DoMove(IPlayMove move)
      {
         if (move is IJVierGewinntMove)
         {
            DoVierGewinntMove((IJVierGewinntMove)move);
            _CurrentPlayer = (_CurrentPlayer == 1) ? 2 : 1;
         }
      }

      public int GetCurrentPlayer()
      {
         return _CurrentPlayer;
      }

      public void DoVierGewinntMove(IJVierGewinntMove move)
      {
         if (move.Row >= 0 && move.Row < GlobalVariables.RowCount && 
             move.Column >= 0 && move.Column < GlobalVariables.ColCount)
         {
            _Field[move.Row, move.Column] = move.PlayerNumber;
         }
      }

      public int CheckIfPLayerWon()
      {
         int winCondition = 4;
         for (int row = 0; row < GlobalVariables.RowCount; row++)
         {
            for (int col = 0; col < GlobalVariables.ColCount; col++)
            {
               int owner = _Field[row, col];
               if (owner > 0)
               {
                  if (col <= GlobalVariables.ColCount - winCondition)
                  {
                     if (CheckDirection(row, col, 0, 1, owner)) return owner;
                  }

                  if (row <= GlobalVariables.RowCount - winCondition)
                  {
                     if (CheckDirection(row, col, 1, 0, owner)) return owner;
                  }

                  if (row <= GlobalVariables.RowCount - winCondition &&
                      col <= GlobalVariables.ColCount - winCondition)
                  {
                     if (CheckDirection(row, col, 1, 1, owner)) return owner;
                  }

                  if (row >= winCondition - 1 && col <= GlobalVariables.ColCount - winCondition)
                  {
                     if (CheckDirection(row, col, -1, 1, owner)) return owner;
                  }
               }
            }
         }
         return -1;
      }

      public bool CheckDirection(int startRow, int startCol, int rowStep, int colStep, int ownerNumber)
      {
         for (int i = 1; i < 4; i++)
         {
            int nextRow = startRow + rowStep * i;
            int nextCol = startCol + colStep * i;

            if (nextRow < 0 || nextRow >= GlobalVariables.RowCount ||
                nextCol < 0 || nextCol >= GlobalVariables.ColCount ||
                _Field[nextRow, nextCol] != ownerNumber)
            {
               return false;
            }
         }
         return true;
      }

      public void ClearField()
      {
         for (int i = 0; i < GlobalVariables.RowCount; i++)
         {
            for (int j = 0; j < GlobalVariables.ColCount; j++)
            {
               _Field[i, j] = 0;
            }
         }
      }
      
   }

   public class J_VierGewinntMove : IJVierGewinntMove
   {
      private int _Row { get; }
      private int _Column { get; }
      private int _PlayerNumber { get; }

      public J_VierGewinntMove(int row, int column, int playerNumber)
      {
         _Row = row;
         _Column = column;
         _PlayerNumber = playerNumber;
      }
      public int Row => _Row;
      public int Column => _Column;
      public int PlayerNumber => _PlayerNumber;
   }

   public class J_VierGewinntHumanPlayer : IJHumanVierGewinntPlayer
   {
      public string Name => "VierGewinnt Player Human";
      public int _PlayerNumber = 0;
      public int PlayerNumber => _PlayerNumber;

      public bool CanBeRuledBy(IGameRules rules)
      {
         return rules is IJVierGewinntRules;
      }

      public void SetPlayerNumber(int playerNumber)
      {
         _PlayerNumber = playerNumber;
      }

      public IGamePlayer Clone()
      {
         J_VierGewinntHumanPlayer vghp = new J_VierGewinntHumanPlayer();
         vghp.SetPlayerNumber(_PlayerNumber);
         return vghp;
      }
      
      public IPlayMove GetMove(IMoveSelection selection, IGameField field)
      {
         if (field is IJVierGewinntField vierGewinntField)
         {
            return GetMove(selection, vierGewinntField);
         }
         return null;
      }

      public IJVierGewinntMove GetMove(IMoveSelection selection, IJVierGewinntField field)
      {
         if (!(selection is IClickSelection sel)) return null;

         for (int col = 0; col < GlobalVariables.ColCount; col++)
         {
            if (sel.XClickPos > (col * 100) && sel.XClickPos < 100 + (col * 100))
            {
               for (int row = GlobalVariables.RowCount - 1; row >= 0; row--)
               {
                  if (field[row, col] == 0)
                  {
                     return new J_VierGewinntMove(row, col, _PlayerNumber);
                  }
               }
            }
         }
         return null;
      }
   }

   public class J_VierGewinntComputerPlayer : IJComputerVierGewinntPlayer
   {
      public string Name => "VierGewinnt Player Computer";
      int _PlayerNumber = 0;
      public int PlayerNumber => _PlayerNumber;
      
      public bool CanBeRuledBy(IGameRules rules)
      {
         return rules is IJVierGewinntRules;
      }

      public void SetPlayerNumber(int playerNumber)
      {
         _PlayerNumber = playerNumber;
      }

      public IGamePlayer Clone()
      {
         J_VierGewinntComputerPlayer vgcp = new J_VierGewinntComputerPlayer();
         vgcp.SetPlayerNumber(_PlayerNumber);
         return vgcp;
      }

      public IPlayMove GetMove(IGameField field)
      {
         if (field is IJVierGewinntField vierGewinntField)
         {
            return GetMove(vierGewinntField);
         }
         return null;
      }

      public IJVierGewinntMove GetMove(IJVierGewinntField field)
      {
         Random rand = new Random();
         int f = rand.Next(0, GlobalVariables.ColCount);
         for (int i = 0; i < GlobalVariables.ColCount; i++)
         {
            int col = (f + i) % GlobalVariables.ColCount;
            
            for (int row = GlobalVariables.RowCount - 1; row >= 0; row--)
            {
               if (field[row, col] == 0)
               {
                  return new J_VierGewinntMove(row, col, _PlayerNumber);
               }
            }
         }
         return null;
      }
   }
}