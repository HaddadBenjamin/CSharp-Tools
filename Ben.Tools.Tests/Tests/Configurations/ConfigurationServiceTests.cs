using System;
using System.Threading.Tasks;
using BenTools.Services.Configurations.Light;
using BenTools.Services.Configurations.Normal;
using BenTools.Services.Configurations.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ben.Tools.Tests.Tests.Configurations
{
    [TestClass]
    public class ConfigurationServiceTests
    {
        [TestMethod]
        public void Light_Service_Complete_Test()
        {
            Parallel.For(0, 100, (i, state) =>
            {
                 // La classe de configuration permet l'utilisation de champs requis ou privée et d'écrire et d'utiliser verbeusement vos configurations.
                var configurationClass = new JsonLightConfigurationService().ToClass<ConfigurationSample>("configurationSample");

                Assert.AreEqual("String", configurationClass.String);
                Assert.AreEqual(3, configurationClass.Number);
                Assert.AreEqual(true, configurationClass.Boolean);
                Assert.AreEqual(true, configurationClass.Array.Length.Equals(2));
                Assert.AreEqual(true, configurationClass.RequieredField != null);
                Assert.AreEqual("PrivateSetter", configurationClass.PrivateSetter);
                Assert.AreEqual("String", configurationClass.Class.String);
                Assert.AreEqual(true, configurationClass.FieldThatDontExistInDefaultConfiguration != null);
                Assert.AreEqual("overrided", configurationClass.OverrideField);

                var optionsWithoutMerge = new ConfigurationOptions(mergeConfiguration: false);
                var configurationClassNotMerged = new JsonLightConfigurationService(optionsWithoutMerge).ToClass<NotMergedConfigurationSample>("notMergedConfigurationSample");

                Assert.AreEqual("not overrided", configurationClassNotMerged.OverrideField);

                var optionsForceEnvironmentWithoutMerge = new ConfigurationOptions(mergeConfiguration: false, forcedCurrentEnvironment: "Development");
                var configurationClassNotMergedForcedEnvironment = new JsonLightConfigurationService(optionsForceEnvironmentWithoutMerge).ToClass<NotMergedConfigurationSample>("notMergedConfigurationSample");
                Assert.AreEqual("forced environment", configurationClassNotMergedForcedEnvironment.OverrideField);
            });
        }

        [TestMethod]
        public void Normal_Service_Configuration_Root_Complete_Test()
        {
            // La racine de configuration permet d'éviter de définir une classe de mappage à votre fichier de configuration pour utiliser votre configuration rapidement.
            var configurationRoot = new JsonConfigurationService().ToRoot("configurationSample");

            // Voici les différentes façons d'accéder à vos champs de configuration sans passer par une classe intermédiaire.
            Assert.AreEqual("String", configurationRoot["String"]);                 // Accèder à un champ.
            Assert.AreEqual(3, Convert.ToInt32(configurationRoot["Number"]));
            Assert.AreEqual(true, Convert.ToBoolean(configurationRoot["Boolean"]));
            Assert.AreEqual(true, configurationRoot.GetSection("Array").Get<string[]>().Length.Equals(2));
            Assert.AreEqual("a", configurationRoot["Array:0"]);                     // Accéder à un élément d'un tableau à l'index n.
            Assert.AreEqual(true, configurationRoot["RequieredField"] != null);
            Assert.AreEqual("PrivateSetter", configurationRoot["PrivateSetter"]);
            Assert.AreEqual("String", configurationRoot["Class:String"]);           // Accéder à un champ d'une classe. 
            Assert.AreEqual(true, configurationRoot["FieldThatDontExistInDefaultConfiguration"] != null);
            Assert.AreEqual(true, configurationRoot["OverrideField"] != "not overrided");
        }

        [TestMethod, ExpectedException(typeof(Newtonsoft.Json.JsonSerializationException))]
        public void Configuration_Service_Is_Requiered_Test()
        {
            // Ce test montre l'efficacité de la contrainte sur les champs requis.
            var emptyConfiguration = new JsonConfigurationService().ToClass<ConfigurationSample>("emptyConfigurationSample");
        }
    }
}
