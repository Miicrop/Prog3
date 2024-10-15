using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace OOPGames 
{
    public interface IJPaintVierGewinnt : IPaintGame 
    {
        void PaintVierGewinntField(Canvas canvas, IJVierGewinntField currentField);
    }

    public interface IJVierGewinntField : IGameField
    {
        int this[int row, int col] { get; set; }
    }

    public interface IJVierGewinntMove : IRowMove, IColumnMove
    {
        
    }

    public interface IJVierGewinntRules : IGameRules
    {
        IJVierGewinntField VierGewinntField { get; }
        void DoVierGewinntMove(IJVierGewinntMove move);
    }

    public interface IJHumanVierGewinntPlayer : IHumanGamePlayer
    {
        IJVierGewinntMove GetMove(IMoveSelection selection, IJVierGewinntField field);
    }

    public interface IJComputerVierGewinntPlayer : IComputerGamePlayer
    {
        IJVierGewinntMove GetMove(IJVierGewinntField field);
    }
}
