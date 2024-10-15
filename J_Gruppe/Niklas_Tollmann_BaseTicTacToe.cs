using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace OOPGames
{
    public abstract class J_BaseTicTacToePaint : I_J_PaintTicTacToe
    {
        public abstract string Name { get; }
        public abstract void PaintTicTacToeField(Canvas canvas, I_J_TicTacToeField currentField);
        public void PaintGameField(Canvas canvas, IGameField currentField)
        {
            if (currentField is I_J_TicTacToeField)
            {
                PaintTicTacToeField(canvas, (I_J_TicTacToeField)currentField);
            }
        }
    }

    public abstract class J_BaseTicTacToeField : I_J_TicTacToeField
    {
        public abstract int this[int r, int c] { get; set; }

        public bool CanBePaintedBy(IPaintGame painter)
        {
            return painter is I_J_PaintTicTacToe;
        }
    }

    public abstract class J_BaseTicTacToeRules : I_J_TicTacToeRules
    {
        public abstract I_J_TicTacToeField TicTacToeField { get; }
        public abstract bool MovesPossible { get; }
        public abstract string Name { get; }
        public abstract int CheckIfPLayerWon();
        public abstract void ClearField();
        public abstract void DoTicTacToeMove(I_J_TicTacToeMove move);
        public IGameField CurrentField => TicTacToeField;

        public void DoMove(IPlayMove move)
        {
            if (move is I_J_TicTacToeMove)
            {
                DoTicTacToeMove((I_J_TicTacToeMove)move);
            }
        }
    }

    public abstract class J_BaseHumanTicTacToePlayer : I_J_HumanTicTacToePlayer
    {
        public abstract string Name { get; }
        public abstract int PlayerNumber { get; }
        public abstract I_J_TicTacToeMove GetMove(IMoveSelection selection, I_J_TicTacToeField field);
        public abstract void SetPlayerNumber(int playerNumber);
        public abstract IGamePlayer Clone();
        public bool CanBeRuledBy(IGameRules rules)
        {
            return rules is I_J_TicTacToeRules;
        }
        public IPlayMove GetMove(IMoveSelection selection, IGameField field)
        {
            if (field is I_J_TicTacToeField)
            {
                return GetMove(selection, (I_J_TicTacToeField)field);
            }
            else
            {
                return null;
            }
        }
    }
    
    public abstract class J_BaseComputerTicTacToePlayer : I_J_ComputerTicTacToePlayer 
    {
        public abstract string Name { get; }
        public abstract int PlayerNumber { get; }
        public abstract void SetPlayerNumber(int playerNumber);
        public abstract I_J_TicTacToeMove GetMove(I_J_TicTacToeField field);
        public abstract IGamePlayer Clone();
        public bool CanBeRuledBy(IGameRules rules)
        {
            return rules is I_J_TicTacToeRules;
        }
        public IPlayMove GetMove(IGameField field)
        {
            if (field is I_J_TicTacToeField)
            {
                return GetMove((I_J_TicTacToeField)field);
            }
            else
            {
                return null;
            }
        }
    }
}
