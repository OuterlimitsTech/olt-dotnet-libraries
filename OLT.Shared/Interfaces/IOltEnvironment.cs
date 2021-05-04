namespace OLT.Core
{
    public interface IOltEnvironment
    {

        /// <summary>
        /// Checks if the current host environment name is <see cref="OltEnvironments.Development"/>.
        /// </summary>
        /// <returns>True if the environment name is <see cref="OltEnvironments.Development"/>, otherwise false.</returns>
        bool IsDevelopment { get; }

        /// <summary>
        /// Checks if the current host environment name is <see cref="OltEnvironments.Test"/>.
        /// </summary>
        /// <returns>True if the environment name is <see cref="OltEnvironments.Test"/>, otherwise false.</returns>
        bool IsTest { get; }

        /// <summary>
        /// Checks if the current host environment name is <see cref="OltEnvironments.Staging"/>.
        /// </summary>
        /// <returns>True if the environment name is <see cref="OltEnvironments.Staging"/>, otherwise false.</returns>
        bool IsStaging { get; }

        /// <summary>
        /// Checks if the current host environment name is <see cref="OltEnvironments.Production"/>.
        /// </summary>
        /// <returns>True if the environment name is <see cref="OltEnvironments.Production"/>, otherwise false.</returns>
        bool IsProduction { get; }


    }
}