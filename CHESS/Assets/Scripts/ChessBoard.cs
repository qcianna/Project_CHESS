using UnityEngine;

/// <summary>
/// ChessBoard class storing info about chess board
/// </summary>
public class ChessBoard : MonoBehaviour {

    [Header("Appearance")]
    [SerializeField] private Material tileMaterial;

    [Header("Objects")]
    [SerializeField] private GameObject[] chessPiecesObjects;
    [SerializeField] private Material[] teamMaterials;

    private const int TILE_COUNT_X = 8;
    private const int TILE_COUNT_Y = 8;
    
    private GameObject[,] tiles;
    private ChessPiece[,] chessPieces;
    
    private void Awake() {
        GenerateTiles(1);
        SpawnPieces();
        PositionPieces();
    }

    /// <summary>
    /// generate tiles
    /// </summary>
    /// <param name="tileSize">size of one square tile</param>
    private void GenerateTiles(float tileSize) {
        tiles = new GameObject[TILE_COUNT_X, TILE_COUNT_Y];
        for (int x = 0; x < TILE_COUNT_X; x++) {
            for(int y = 0; y < TILE_COUNT_Y; y++) {
                tiles[x, y] = GenerateSingleTile(x, y, tileSize);
            }
        }
    }

    /// <summary>
    /// generate single tile
    /// </summary>
    /// <param name="x">position x</param>
    /// <param name="y">position y</param>
    /// <param name="tileSize">size of one square tile</param>
    /// <returns></returns>
    private GameObject GenerateSingleTile(int x, int y, float tileSize) {
        GameObject tile = new GameObject("x:{" + x + "}, y:{" + y + "}");
        tile.transform.parent = transform;

        Mesh mesh = new Mesh();
        tile.AddComponent<MeshFilter>().mesh = mesh;
        tile.AddComponent<MeshRenderer>().material = tileMaterial;

        Vector3[] vertices = new Vector3[4];
        vertices[0] = new Vector3(x * tileSize, 1, y * tileSize);
        vertices[1] = new Vector3(x * tileSize, 1, (y+1) * tileSize);
        vertices[2] = new Vector3((x+1) * tileSize, 1, y * tileSize);
        vertices[3] = new Vector3((x+1) * tileSize, 1, (y+1) * tileSize);

        int[] tris = new int[] {0, 1, 2, 1, 3, 2 };

        mesh.vertices = vertices;
        mesh.triangles = tris;
        mesh.RecalculateNormals();

        tile.AddComponent<BoxCollider>();

        return tile;
    }

    /// <summary>
    /// spawn all pieces
    /// </summary>
    private void SpawnPieces() {
        chessPieces = new ChessPiece[TILE_COUNT_X, TILE_COUNT_Y];

        chessPieces[0, 0] = SpawnSinglePiece(ChessPieceType.Rook, Team.White);
        chessPieces[1, 0] = SpawnSinglePiece(ChessPieceType.Knight, Team.White);
        chessPieces[2, 0] = SpawnSinglePiece(ChessPieceType.Bishop, Team.White);
        chessPieces[3, 0] = SpawnSinglePiece(ChessPieceType.Queen, Team.White);
        chessPieces[4, 0] = SpawnSinglePiece(ChessPieceType.King, Team.White);
        chessPieces[5, 0] = SpawnSinglePiece(ChessPieceType.Bishop, Team.White);
        chessPieces[6, 0] = SpawnSinglePiece(ChessPieceType.Knight, Team.White);
        chessPieces[7, 0] = SpawnSinglePiece(ChessPieceType.Rook, Team.White);
        for(int i=0; i<TILE_COUNT_X; i++) {
            chessPieces[i, 1] = SpawnSinglePiece(ChessPieceType.Pawn, Team.White);
        }

        chessPieces[0, 7] = SpawnSinglePiece(ChessPieceType.Rook, Team.Black);
        chessPieces[1, 7] = SpawnSinglePiece(ChessPieceType.Knight, Team.Black);
        chessPieces[2, 7] = SpawnSinglePiece(ChessPieceType.Bishop, Team.Black);
        chessPieces[3, 7] = SpawnSinglePiece(ChessPieceType.Queen, Team.Black);
        chessPieces[4, 7] = SpawnSinglePiece(ChessPieceType.King, Team.Black);
        chessPieces[5, 7] = SpawnSinglePiece(ChessPieceType.Bishop, Team.Black);
        chessPieces[6, 7] = SpawnSinglePiece(ChessPieceType.Knight, Team.Black);
        chessPieces[7, 7] = SpawnSinglePiece(ChessPieceType.Rook, Team.Black);
        for (int i = 0; i < TILE_COUNT_X; i++) {
            chessPieces[i, 6] = SpawnSinglePiece(ChessPieceType.Pawn, Team.Black);
        }
    }

    /// <summary>
    /// set position of pieces on chess board
    /// </summary>
    private void PositionPieces() {
        for (int x=0; x<TILE_COUNT_X; x++) {
            for (int y=0; y<TILE_COUNT_Y; y++) {
                if(chessPieces[x, y] != null) {
                    PositionSinglePiece(x, y);
                }  
            }
        }
    }

    /// <summary>
    /// set chess piece position on a center of a tail 
    /// </summary>
    /// <param name="x">x coordinate</param>
    /// <param name="y">y coordinate</param>
    private void PositionSinglePiece(int x, int y) {
        chessPieces[x, y].setCurrentPositionX(x);
        chessPieces[x, y].setCurrentPositionY(y);
        chessPieces[x, y].transform.position = GetTileCenter(x, y);
    }

    private Vector3 GetTileCenter(int x, int y) {
        return new Vector3(x, 1, y) + new Vector3(0.5f, 0, 0.5f);
    }

    private ChessPiece SpawnSinglePiece(ChessPieceType type, Team team) {
        ChessPiece piece = Instantiate(chessPiecesObjects[(int)type - 1], transform).GetComponent<ChessPiece>();

        piece.setType(type);
        piece.setTeam(team);
        piece.GetComponent<MeshRenderer>().material = teamMaterials[(int)team - 1];

        return piece;
    }
}
