namespace TBSim;

public class Path
{
    public List<Phase> Phases { get; set; } = [];

    //public void AddPhase(GameBoard board)
    //{
    //    Phases.Add(new Phase(board.DarkSide, board.Mixed, board.LightSide));
    //}

    public void AddPhase(Phase phase)
    {
        Phases.Add(phase);
    }

    public void AddPhases(IEnumerable<Phase> phases) => Phases.AddRange(phases);

    public void Append(Path path) => Phases.AddRange(path.Phases);

    public int GetStars() => Phases.Sum(x => x.GetStars());
}
