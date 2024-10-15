using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace OOPGames 
{
    public interface I_J_PaintTicTacToe : IPaintGame 
    {
        void PaintTicTacToeField(Canvas canvas, I_J_TicTacToeField currentField);
    }

    public interface I_J_TicTacToeField : IGameField
    {
        int this[int row, int col] { get; set; }
    }

    public interface I_J_TicTacToeMove : IRowMove, IColumnMove
    {
        
    }

    public interface I_J_TicTacToeRules : IGameRules
    {
        I_J_TicTacToeField TicTacToeField { get; }
        void DoTicTacToeMove(I_J_TicTacToeMove move);
    }

    public interface I_J_HumanTicTacToePlayer : IHumanGamePlayer
    {
        I_J_TicTacToeMove GetMove(IMoveSelection selection, I_J_TicTacToeField field);
    }

    public interface I_J_ComputerTicTacToePlayer : IComputerGamePlayer
    {
        I_J_TicTacToeMove GetMove(I_J_TicTacToeField field);
    }
}
