// https://code.google.com/codejam/contest/6214486/dashboard#s=p2

#include <iostream>
#include <queue>
#include <string>
#include <tuple>
#include <unordered_map>
#include <unordered_set>
#include <vector>

using namespace std;

void process(istream &input, ostream &output)
{
    int ticket_count;
    input >> ticket_count;

    unordered_set<string> destinations;
    unordered_map<string, string> tickets;

    for (int i = 0; i != ticket_count; ++i)
    {
        string from, to;

        input >> from >> to;

        destinations.emplace(to);
        tickets.emplace(move(from), move(to));
    }

    for (auto &ticket : tickets)
    {
        if (destinations.count(ticket.first) == 0)
        {
            string current = ticket.first;

            for (; tickets.count(current) > 0; current = tickets[current])
            {
                output << ' ' << current << '-' << tickets[current];
            }
        }
    }

    output << endl;
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
