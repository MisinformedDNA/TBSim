namespace TBSim;

public abstract record GameAction();
public record PlayerDeploymentAction(string PlayerName, int GalacticPower) : GameAction;
public record GuildDeploymentAction(int GalacticPower) : GameAction;
