// https://code.google.com/codejam/contest/6214486/dashboard#s=p3

#include <array>
#include <iostream>
#include <queue>
#include <string>
#include <tuple>
#include <unordered_map>
#include <unordered_set>
#include <vector>

using namespace std;

static const int board_size = 8;

int rook_kill(const array<array<char, board_size>, board_size> &board, const pair<int, int> &piece)
{
    int result = 0;

    for (int i = piece.second - 1; i >= 0; --i)
    {
        if (board[piece.first][i] != '\0')
        {
            ++result;
            break;
        }
    }
    for (int i = piece.second + 1; i < board_size; ++i)
    {
        if (board[piece.first][i] != '\0')
        {
            ++result;
            break;
        }
    }
    for (int i = piece.first - 1; i >= 0; --i)
    {
        if (board[i][piece.second] != '\0')
        {
            ++result;
            break;
        }
    }
    for (int i = piece.first + 1; i < board_size; ++i)
    {
        if (board[i][piece.second] != '\0')
        {
            ++result;
            break;
        }
    }

    return result;
}

int bishop_kill(const array<array<char, board_size>, board_size> &board, const pair<int, int> &piece)
{
    int result = 0;

    for (int i = piece.first - 1, j = piece.second - 1; i >= 0 && j >= 0; --i, --j)
    {
        if (board[i][j] != '\0')
        {
            ++result;
            break;
        }
    }
    for (int i = piece.first + 1, j = piece.second + 1; i < board_size && j < board_size; ++i, ++j)
    {
        if (board[i][j] != '\0')
        {
            ++result;
            break;
        }
    }
    for (int i = piece.first - 1, j = piece.second + 1; i >= 0 && j < board_size; --i, ++j)
    {
        if (board[i][j] != '\0')
        {
            ++result;
            break;
        }
    }
    for (int i = piece.first + 1, j = piece.second - 1; i < board_size && j >= 0; ++i, --j)
    {
        if (board[i][j] != '\0')
        {
            ++result;
            break;
        }
    }

    return result;
}

void process(istream &input, ostream &output)
{
    array<array<char, board_size>, board_size> board = {};
    int piece_count;

    input >> piece_count;

    vector<pair<int, int>> pieces;
    pieces.reserve(piece_count);

    for (int i = 0; i != piece_count; ++i)
    {
        string line;
        input >> line;

        pieces.emplace_back(line[0] - 'A', line[1] - '1');
        board[pieces.back().first][pieces.back().second] = line[3];
    }

    int kill_ways = 0;

    for (auto &piece : pieces)
    {
        switch (board[piece.first][piece.second])
        {
            case 'K':
                for (int i = -1; i <= 1; ++i)
                {
                    for (int j = -1; j <= 1; ++j)
                    {
                        if ((i != 0 || j != 0) &&
                            piece.first + i >= 0 && piece.first + i < board_size &&
                            piece.second + j >= 0 && piece.second + j < board_size &&
                            board[piece.first + i][piece.second + j] != '\0')
                        {
                            ++kill_ways;
                        }
                    }
                }
                break;

            case 'Q':
                kill_ways += rook_kill(board, piece) + bishop_kill(board, piece);
                break;

            case 'R':
                kill_ways += rook_kill(board, piece);
                break;

            case 'B':
                kill_ways += bishop_kill(board, piece);
                break;

            case 'N':
                for (auto i : { -2, -1, 1, 2 })
                {
                    for (auto j : { -2, -1, 1, 2 })
                    {
                        // Can over take?
                        if (abs(i) != abs(j) &&
                            piece.first + i >= 0 && piece.first + i < board_size &&
                            piece.second + j >= 0 && piece.second + j < board_size &&
                            board[piece.first + i][piece.second + j] != '\0')
                        {
                            ++kill_ways;
                        }
                    }
                }
                break;

            case 'P':
                if (piece.first + 1 < board_size)
                {
                    if (piece.second > 0 && board[piece.first + 1][piece.second - 1] != '\0')
                    {
                        ++kill_ways;
                    }
                    if (piece.second < board_size - 1 && board[piece.first + 1][piece.second + 1] != '\0')
                    {
                        ++kill_ways;
                    }
                }
                break;
        }
    }

    output << ' ' << kill_ways << '\n';
}

int main()
{
    istream &input = cin;
    ostream &output = cout;

    int case_count;
    input >> case_count;

    for (int i = 1; i <= case_count; ++i)
    {
        output << "Case #" << i << ":";
        process(input, output);
    }
}
