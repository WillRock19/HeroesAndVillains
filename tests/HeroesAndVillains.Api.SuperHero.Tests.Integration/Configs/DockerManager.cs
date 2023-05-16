using Docker.DotNet;
using Docker.DotNet.Models;

namespace HeroesAndVillains.Api.SuperHero.Tests.Integration.Configs
{
    internal class DockerManager : IDisposable
    {
        private const string _runningState = "running";
        private readonly DockerClient _dockerClient;

        public DockerManager() 
        {
            _dockerClient = new DockerClientConfiguration().CreateClient();
        }

        public async Task<bool> ContainersWithDatabasesAreUp(IEnumerable<string> containerNames) 
        {
            var containers = await _dockerClient.Containers.ListContainersAsync(
                new ContainersListParameters { All = true }
            );

            return containerNames.All(name => containers.Any(x => x.Names.First().Contains(name) && x.State == _runningState));
        }

        public void Dispose() 
        {
            _dockerClient.Dispose();
        }
    }
}
