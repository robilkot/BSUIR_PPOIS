namespace LW3.Logic
{
    [Serializable]
    class Passenger
    {
        public string Name { get; init; } = string.Empty;
        public string Surname { get; init; } = string.Empty;
        public int PassNumber { get; init; } = 0;

        public List<Ticket> Tickets = new();

        public Passenger(string name, string surname, int passnumber)
        {
            Name = name;
            Surname = surname;
            PassNumber = passnumber;
        }
    }
}
