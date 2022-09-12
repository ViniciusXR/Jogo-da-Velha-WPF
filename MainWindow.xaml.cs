using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Jogo_da_Velha
{
    public partial class MainWindow : Window
    {
        #region Membros Privados

        private MarkType[] mResults;

        private bool mPlayer1Turn;

        private bool mGameEnded;

        #endregion
        public MainWindow()
        {
            InitializeComponent();

            NewGame();
        }

        private void NewGame()
        {
            mResults = new MarkType[9];

            //Coloca todas as células do vetor em branco
            for (int i = 0; i < mResults.Length; i++)
            {
                mResults[i] = MarkType.Free;
            }

            //Define o primeiro jogador
            mPlayer1Turn = true;

            Container.Children.Cast<Button>().ToList().ForEach(button =>
            {
                //Define as propriedades das células
                button.Content = string.Empty;
                button.Background = Brushes.White;
                button.Foreground = Brushes.Blue;
            });

            mGameEnded = false;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (mGameEnded)
            {
                NewGame();
                return;
            }

            var button = (Button)sender;

            var column = Grid.GetColumn(button);
            var row = Grid.GetRow(button);

            var index = column + (row * 3);

            //Não irá fazer nada se já tiver um valor na célula 
            if (mResults[index] != MarkType.Free)
            {
                return;
            }

            //Irá insirir um valor na célula de acordo com a marcação do jogador(X , O)
            if (mPlayer1Turn)
            {
                mResults[index] = MarkType.Cross;
            }
            else
            {
                mResults[index] = MarkType.Nought;
            }

            //Define o tipo de caractere que será insirido na célula
            if (mPlayer1Turn)//button.Content = mPlayer1Turn ? "X" : "O";
            {
                button.Content = "X";
            }
            else
            {
                button.Content = "O";
            }

            if (!mPlayer1Turn)
            {
                button.Foreground = Brushes.Red;
            }

            //Muda a vez do jogador
            if (mPlayer1Turn)//mPlayerTurn ^= true;
            {
                mPlayer1Turn = false;
            }
            else
            {
                mPlayer1Turn = true;
            }

            //Verifica se temos um vencendor
            CheckForWinner();
        }

        private void CheckForWinner()
        {
            //Verifica por vitórias horizontais
            //Linha 0
            if (mResults[0] != MarkType.Free && (mResults[0] & mResults[1] & mResults[2]) == mResults[0])
            {
                //O jogo terminou
                mGameEnded = true;

                //Destaca as células vencedoras em cor verde
                Button0_0.Background = Button1_0.Background = Button2_0.Background = Brushes.Green;
            }

            //Verifica por vitórias horizontais
            //Linha 1
            if (mResults[3] != MarkType.Free && (mResults[3] & mResults[4] & mResults[5]) == mResults[3])
            {
                //O jogo terminou
                mGameEnded = true;

                //Destaca as células vencedoras em cor verde
                Button0_1.Background = Button1_1.Background = Button2_1.Background = Brushes.Green;
            }

            //Verifica por vitórias horizontais
            //Linha 2
            if (mResults[6] != MarkType.Free && (mResults[6] & mResults[7] & mResults[8]) == mResults[6])
            {
                //O jogo terminou
                mGameEnded = true;

                //Destaca as células vencedoras em cor verde
                Button0_2.Background = Button1_2.Background = Button2_2.Background = Brushes.Green;
            }


            //Verifica por vitórias verticais
            //Coluna 0
            if (mResults[0] != MarkType.Free && (mResults[0] & mResults[3] & mResults[6]) == mResults[0])
            {
                //O jogo terminou
                mGameEnded = true;

                //Destaca as células vencedoras em cor verde
                Button0_0.Background = Button0_1.Background = Button0_2.Background = Brushes.Green;
            }

            //Verifica por vitórias verticais
            //Coluna 1
            if (mResults[1] != MarkType.Free && (mResults[1] & mResults[4] & mResults[7]) == mResults[1])
            {
                //O jogo terminou
                mGameEnded = true;

                //Destaca as células vencedoras em cor verde
                Button1_0.Background = Button1_1.Background = Button1_2.Background = Brushes.Green;
            }

            //Verifica por vitórias verticais
            //Coluna 2
            if (mResults[2] != MarkType.Free && (mResults[2] & mResults[5] & mResults[8]) == mResults[2])
            {
                //O jogo terminou
                mGameEnded = true;

                //Destaca as células vencedoras em cor verde
                Button2_0.Background = Button2_1.Background = Button2_2.Background = Brushes.Green;
            }

            //Verifica por vitórias na diagonal
            if (mResults[0] != MarkType.Free && (mResults[0] & mResults[4] & mResults[8]) == mResults[0])
            {
                //O jogo terminou
                mGameEnded = true;

                //Destaca as células vencedoras em cor verde
                Button0_0.Background = Button1_1.Background = Button2_2.Background = Brushes.Green;
            }

            //Verifica por vitórias na diagonal
            if (mResults[2] != MarkType.Free && (mResults[2] & mResults[4] & mResults[6]) == mResults[2])
            {
                //O jogo terminou
                mGameEnded = true;

                //Destaca as células vencedoras em cor verde
                Button2_0.Background = Button1_1.Background = Button0_2.Background = Brushes.Green;
            }

            //Verifica se não tivemos vencedor
            if (!mResults.Any(f => f == MarkType.Free))
            {
                mGameEnded = true;

                //Deixa todas as células na cor laranja
                Container.Children.Cast<Button>().ToList().ForEach(button =>
                {
                    button.Background = Brushes.Orange;
                });
            }
        }
    }
}
