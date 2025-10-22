namespace SenderApiInterface.Models;

public record StartVmRequest(string Password, string VmName);
public record StopVmRequest(string Password, string VmName);
public record DeleteVmRequest(string Password, string VmName);
public record CreateVmRequest(string Password, string TemplateName);