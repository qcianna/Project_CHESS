using UnityEngine;

/// <summary>
/// Enum class for types of chess pieces
/// </summary>
public enum ChessPieceType {
    None = 0,
    Pawn = 1,
    Rook = 2,
    Knight = 3,
    Bishop = 4,
    Queen = 5,
    King = 6
}

/// <summary>
/// Enum class for colors of teams
/// </summary>
public enum Team {
    None = 0,
    White = 1,
    Black = 2
}

/// <summary>
/// ChessPiece class storing info about chess piece
/// </summary>
public class ChessPiece : MonoBehaviour {

    protected ChessPieceType type;
    protected Team team;
    protected int currentPositionX;
    protected int currentPositionY;

    public void setType(ChessPieceType type) {
        this.type = type;
    }

    public void setTeam(Team team) {
        this.team = team;
    }

    public void setCurrentPositionX(int currentPositionX) {
        this.currentPositionX = currentPositionX;
    }

    public void setCurrentPositionY(int currentPositionY) {
        this.currentPositionY = currentPositionY;
    }
}
