// See https://aka.ms/new-console-template for more information

using Lunatic.Domain.Entities;
using Lunatic.Infrastructure;
using Lunatic.Infrastructure.Repositories;

var user = User.Create("lunatic", "lunatic", Role.ADMIN);
Console.WriteLine(user.Value.Username);
Console.WriteLine(user.Value.Password);
var context = new LunaticContext();
var userRepository = new UserRepository(context);
var result = await userRepository.Add(user.Value);
Console.WriteLine(result.Value.Username);
Console.WriteLine(result.Value.Password);
