using Checkers.Model;
using Checkers.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shell;

namespace Checkers.Services
{
    class GameBusinessLogic
    {
        private bool playerJumped = false;
        private bool playerTurn = true;//true=white,false=red
        private bool playerHasMoved = false;
        private bool gameEnded = false;
        private bool canJump;
        private int redPieces = 12;
        private int whitePieces = 12;
        private bool switchTextShwon=false;
        private DamaViewModel pieceJumped;
        private int whiteWins = 0;
        private int redWins = 0;
        public void setCanJump(bool jump)
        {
            canJump = jump;
        }
        public bool getPlayerHasMoved()
        {
            return playerHasMoved;
        }
        public bool getPlayerTurn()
        {
            return playerTurn;
        }
        public bool getPlayerJumped()
        {
            return playerJumped;
        }
        public DamaViewModel getPieceJumped()
        {
            return pieceJumped;
        }
        public void CountPieces(ObservableCollection<DamaViewModel> pieces)
        {
            redPieces = 0;
            whitePieces = 0;
            foreach(DamaViewModel piece in pieces)
            {
                if (piece.Color == Brushes.Red)
                    redPieces += 1;
                else
                    whitePieces += 1;
            }
        }
        public List<Tuple<int, int>> GetValidMoves(Dama piece, Board board,ObservableCollection<DamaViewModel> Pieces)
        {
            List<Tuple<int, int>> validMoves = new List<Tuple<int, int>>();
            if (piece.PieceColor == PieceColor.White && piece.IsKing==false)
            {
                validMoves=GetValidMovesWhite(piece, board, Pieces);
            }
            else if (piece.PieceColor == PieceColor.Red && piece.IsKing==false)
            {
                validMoves=GetValidMovesRed(piece, board, Pieces); 
            }
            else if(piece.IsKing)
            {
                List<Tuple<int, int>> validMovesWhite = GetValidMovesWhite(piece, board, Pieces);
                List<Tuple<int, int>> validMovesRed = GetValidMovesRed(piece, board, Pieces);

                validMoves.AddRange(validMovesWhite);
                validMoves.AddRange(validMovesRed);
            }

            return validMoves;
        }
        public List<Tuple<int, int>> GetValidMovesRed(Dama piece, Board board, ObservableCollection<DamaViewModel> Pieces)
        {
            List<Tuple<int, int>> validMoves = new List<Tuple<int, int>>();
            // Check if the piece is not on the first row
            if (canJump && playerJumped && piece.Column != pieceJumped.Column && piece.Row != pieceJumped.Row)
            {
                MessageBox.Show("Please select the piece that jumped or switch turn!");
                switchTextShwon= true;
                return validMoves;
            }
            if (piece.Row > 0)
            {
                // For red pieces, they can move left up and right up
                if (piece.Column > 0 && piece.Row > 0 && board.GetDamaAtPosition(piece.Row - 1, piece.Column - 1) == null && !playerJumped)
                    validMoves.Add(new Tuple<int, int>(piece.Row - 1, piece.Column - 1)); // Left up
                else
                    if (piece.Column > 1 && piece.Row > 1 && board.GetDamaAtPosition(piece.Row - 2, piece.Column - 2) == null)
                {
                    if (board.GetDamaAtPosition(piece.Row - 1, piece.Column - 1) != null && board.GetDamaAtPosition(piece.Row - 1, piece.Column - 1).PieceColor != piece.PieceColor)
                    {
                        validMoves.Add(new Tuple<int, int>(piece.Row - 2, piece.Column - 2));
                    }
                }
                if (piece.Column < 7 && piece.Row > 0 && board.GetDamaAtPosition(piece.Row - 1, piece.Column + 1) == null && !playerJumped)
                    validMoves.Add(new Tuple<int, int>(piece.Row - 1, piece.Column + 1)); // Right up
                else
                    if (piece.Column < 6 && piece.Row > 1 && board.GetDamaAtPosition(piece.Row - 2, piece.Column + 2) == null)
                {
                    if (board.GetDamaAtPosition(piece.Row - 1, piece.Column + 1)!=null && board.GetDamaAtPosition(piece.Row - 1, piece.Column + 1).PieceColor != piece.PieceColor)
                    {
                        validMoves.Add(new Tuple<int, int>(piece.Row - 2, piece.Column + 2));
                    }
                }
            }
            return validMoves;
        }
        public List<Tuple<int, int>> GetValidMovesWhite(Dama piece, Board board, ObservableCollection<DamaViewModel> Pieces)
        {
            List<Tuple<int, int>> validMoves = new List<Tuple<int, int>>();
            // Check if the piece is not on the last row
            if (canJump && playerJumped && piece.Column != pieceJumped.Column && piece.Row != pieceJumped.Row)
            {
                MessageBox.Show("Please select the piece that jumped or switch turn!");
                switchTextShwon = true;
                return validMoves;
            }
            if (piece.Row < 7)
            {
                // For white pieces, they can move left down and right down
                if (piece.Column > 0 && piece.Row < 7 && board.GetDamaAtPosition(piece.Row + 1, piece.Column - 1) == null && !playerJumped)
                    validMoves.Add(new Tuple<int, int>(piece.Row + 1, piece.Column - 1)); // Left down
                else
                    if (piece.Column > 1 && piece.Row < 6 && board.GetDamaAtPosition(piece.Row + 2, piece.Column - 2) == null)
                {
                    if (board.GetDamaAtPosition(piece.Row + 1, piece.Column - 1) != null && board.GetDamaAtPosition(piece.Row + 1, piece.Column - 1).PieceColor != piece.PieceColor)
                    {
                        validMoves.Add(new Tuple<int, int>(piece.Row + 2, piece.Column - 2));
                    }
                }
                if (piece.Column < 7 && piece.Row < 7 && board.GetDamaAtPosition(piece.Row + 1, piece.Column + 1) == null && !playerJumped)
                    validMoves.Add(new Tuple<int, int>(piece.Row + 1, piece.Column + 1)); // Right down
                else
                    if (piece.Column < 6 && piece.Row < 6 && board.GetDamaAtPosition(piece.Row + 2, piece.Column + 2) == null)
                {
                    if (board.GetDamaAtPosition(piece.Row + 1, piece.Column + 1) != null && board.GetDamaAtPosition(piece.Row + 1, piece.Column + 1).PieceColor != piece.PieceColor)
                    {
                        validMoves.Add(new Tuple<int, int>(piece.Row + 2, piece.Column + 2));
                    };
                }
            }
            return validMoves;
        }
        public void removePieceLeftDown(ObservableCollection<DamaViewModel> Pieces,Dama piece,Board board)
        {
            board.RemoveDama(piece.Row + 1, piece.Column - 1);
            var capturedPieceViewModel = Pieces.FirstOrDefault(p => p.Row == piece.Row + 1 && p.Column == piece.Column - 1);
            if (capturedPieceViewModel != null)
            {
                Pieces.Remove(capturedPieceViewModel);
            }
        }
        public void removePieceRightDown(ObservableCollection<DamaViewModel> Pieces, Dama piece, Board board)
        {
            board.RemoveDama(piece.Row + 1, piece.Column + 1);
            var capturedPieceViewModel = Pieces.FirstOrDefault(p => p.Row == piece.Row + 1 && p.Column == piece.Column + 1);
            if (capturedPieceViewModel != null)
            {
                Pieces.Remove(capturedPieceViewModel);
            }
        }
        public void removePieceLeftUp(ObservableCollection<DamaViewModel> Pieces, Dama piece, Board board)
        {
            board.RemoveDama(piece.Row - 1, piece.Column - 1);
            var capturedPieceViewModel = Pieces.FirstOrDefault(p => p.Row == piece.Row - 1 && p.Column == piece.Column - 1);
            if (capturedPieceViewModel != null)
            {
                Pieces.Remove(capturedPieceViewModel);
            }
        }
        public void removePieceRightUp(ObservableCollection<DamaViewModel> Pieces, Dama piece, Board board)
        {
            board.RemoveDama(piece.Row - 1, piece.Column + 1);
            var capturedPieceViewModel = Pieces.FirstOrDefault(p => p.Row == piece.Row - 1 && p.Column == piece.Column + 1);
            if (capturedPieceViewModel != null)
            {
                Pieces.Remove(capturedPieceViewModel);
            }
        }
        public void UpdateBoard(Board board,BorderViewModel border,DamaViewModel currentPiece, ObservableCollection<BorderViewModel> Cells,ObservableCollection<DamaViewModel> Pieces)
        {
            if(gameEnded)
                return;
            if (currentPiece.Row+2==border.Row)
            {
                if(currentPiece.Column-2==border.Column)
                    removePieceLeftDown(Pieces, board.GetDamaAtPosition(currentPiece.Row, currentPiece.Column), board);
                else
                    if(currentPiece.Column+2==border.Column)
                        removePieceRightDown(Pieces, board.GetDamaAtPosition(currentPiece.Row, currentPiece.Column), board);
                if (currentPiece.Color == Brushes.White)
                    redPieces--;
                else
                    whitePieces--;
                if (canJump)
                {
                    playerJumped = true;
                    pieceJumped = currentPiece;
                }
            }
            if (currentPiece.Row-2 == border.Row)
            {
                if (currentPiece.Column-2 == border.Column)
                    removePieceLeftUp(Pieces, board.GetDamaAtPosition(currentPiece.Row, currentPiece.Column), board);
                else
                    if (currentPiece.Column+2 == border.Column)
                    removePieceRightUp(Pieces, board.GetDamaAtPosition(currentPiece.Row, currentPiece.Column), board);
                if (currentPiece.Color == Brushes.White)
                    redPieces--;
                else
                    whitePieces--;
                if (canJump)
                {
                    playerJumped = true;
                    pieceJumped = currentPiece;
                }
            }
            board.MoveDama(currentPiece.Row, currentPiece.Column, border.Row, border.Column);
            playerHasMoved = true;
            currentPiece.Row = border.Row;
            currentPiece.Column = border.Column;
            Helper.DefaultBorders(Cells);
            if((currentPiece.Color==Brushes.Red && border.Row==0)|| (currentPiece.Color==Brushes.White && border.Row==7)) 
            {
                currentPiece.DisplayR = Visibility.Visible;
                Dama dama=board.GetDamaAtPosition(currentPiece.Row, currentPiece.Column);
                dama.IsKing = true;
            }
            if(redPieces==0)
            {
                MessageBox.Show("White Wins!");
                Tuple<int, int,int> wins = Helper.getWins();
                whiteWins = wins.Item1 + 1;
                int max = 0;
                if (whitePieces > wins.Item3)
                    max = whitePieces;
                else
                    max = wins.Item3;
                Helper.setWins(whiteWins, wins.Item2,max);
                gameEnded=true;
            }
            else if(whitePieces==0)
            {
                MessageBox.Show("Red Wins!");
                Tuple<int, int,int> wins = Helper.getWins();
                redWins = wins.Item1 + 1;
                int max = 0;
                if (redPieces > wins.Item3)
                    max = redPieces;
                else
                    max = wins.Item3;
                Helper.setWins(wins.Item1, redWins,max);
                gameEnded = true;
            }
        }
        public void Moves(List<Tuple<int,int>>validMoves, ObservableCollection<BorderViewModel> Cells)
        {
            if(!switchTextShwon &&validMoves.Count()==0)
            {
                MessageBox.Show("There are no valid moves for this piece");
                return;
            }
            else
            {
                switchTextShwon = false;
               
            }    
            foreach (Tuple<int, int> move in validMoves)
            {
                int targetRow = move.Item1;
                int targetColumn = move.Item2;

                // Calculate index in the Cells collection based on row and column
                int index = targetRow * 8 + targetColumn;

                // Check if index is valid
                if (index >= 0 && index < Cells.Count)
                {
                    // Update the background color of the corresponding cell
                    Cells[index].Background = Brushes.Green;
                }
            }
        }
        public void SwitchTurn()
        {
            playerHasMoved = false;
            playerTurn =! playerTurn;
            pieceJumped = null;
            playerJumped = false;
            switchTextShwon = false;
        }
    }
}