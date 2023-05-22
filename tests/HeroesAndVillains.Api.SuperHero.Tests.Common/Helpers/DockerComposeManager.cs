using Docker.DotNet;
using Docker.DotNet.Models;
using System.Diagnostics;

namespace HeroesAndVillains.Tests.Common.Helpers
{
    public class DockerManager : IDisposable
    {
        private const string _runningState = "running";
        private readonly DockerClient _dockerClient;
        private readonly List<string> _containerNames;
        private readonly string _dockerComposeFilePath;


        public DockerManager(List<string> containerNames, string dockerComposePath) 
        {
            _dockerClient = new DockerClientConfiguration().CreateClient();
            _containerNames = containerNames;
            _dockerComposeFilePath = dockerComposePath;
        }

        public async Task<bool> ContainersWithDatabasesAreUp() 
        {
            var containers = await _dockerClient.Containers.ListContainersAsync(
                new ContainersListParameters { All = true }
            );

            return _containerNames.All(name => containers.Any(x => x.Names.First().Contains(name) && x.State == _runningState));
        }

        public async Task<bool> GuaranteeContainersAreUp() 
            => await ContainersWithDatabasesAreUp() 
            || await StartDockerContainers();

        public async Task StopAndRemoveContainers() 
        {
            foreach (var name in _containerNames) 
            {
                await _dockerClient.Containers.StopContainerAsync(name, new ContainerStopParameters());
                await _dockerClient.Containers.RemoveContainerAsync(name, new ContainerRemoveParameters());
            }
        }

        public void Dispose() 
        {
            _dockerClient.Dispose();
        }

        private async Task<bool> StartDockerContainers() 
        {
            await RunDockerComposeProcess();
            return await ContainersWithDatabasesAreUp();
        }

        private async Task RunDockerComposeProcess() 
        {
            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "docker-compose",
                    Arguments = $"-f {_dockerComposeFilePath} up -d",
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                }
            };

            process.Start();
            await process.WaitForExitAsync();

            if (process.ExitCode != 0)
                throw new Exception("Could not run 'docker-compose' on test's setup. Please, run the command on your test's directory and check what might be happening.");
        }
    }
}
