namespace LW3.Logic
{
    [Serializable]
    class Ticket
    {
        public Flight Flight { get; init; }
        public int PassNumber { get; init; }

        public Ticket(Flight flight, int passNumber)
        {
            Flight = flight;
            PassNumber = passNumber;
        }
    }
}
