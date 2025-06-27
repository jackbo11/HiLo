namespace HiLo.DTOs;

public record NewGameRequest(string playerName);
public record JoinGameRequest(string playerName, string gameKey);
public record ProgressGameRequest(HiLo.Core.Action action);
public record UpdateRoundsRequest(int numberOfRounds);