namespace server.DTos;

public record UserWriteDto(
    string UserName,
    string Email,
    string Password
);
