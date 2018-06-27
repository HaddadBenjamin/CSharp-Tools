using System;
using BenTools.Managers.Configurations.Light;
using BenTools.Managers.Configurations.Options;
using BenTools.Services.Configurations.Light;
using BenTools.Services.Configurations.Light.Options;
using BenTools.Services.Configurations.Normal;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;

namespace BenTools.Tests.Tests.Configurations
{
    [TestFixture]
    public class ConfigurationTests
    {
        [TestCase]
        public void Light_Service_Complete_Test()
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

            var configurationSubSection = new JsonLightConfigurationService().ToClass<Class>("configurationSample", new[] { "SubSection", "Class" });
            Assert.AreEqual("String", configurationSubSection.String);

            var optionsWithoutMerge = new ConfigurationOptions(mergeConfigurationFiles: false);
            var configurationClassNotMerged = new JsonLightConfigurationService(optionsWithoutMerge).ToClass<NotMergedConfigurationSample>("notMergedConfigurationSample");

            Assert.AreEqual("not overrided", configurationClassNotMerged.OverrideField);

            var optionsForceEnvironmentWithoutMerge = new ConfigurationOptions(mergeConfigurationFiles: false, environmentDirectory: "Development");
            var configurationClassNotMergedForcedEnvironment = new JsonLightConfigurationService(optionsForceEnvironmentWithoutMerge).ToClass<NotMergedConfigurationSample>("notMergedConfigurationSample");
            Assert.AreEqual("forced environment", configurationClassNotMergedForcedEnvironment.OverrideField);
        }

        [Test]
        public void Normal_Service_Configuration_Root_Complete_Test()
        {
            // La racine de configuration permet d'éviter de définir une classe de mappage à votre fichier de configuration pour utiliser votre configuration rapidement.
            var configurationRoot = new JsonConfigurationService().ToConfigurationRoot("configurationSample");

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
            Assert.AreEqual(true, configurationRoot["SubSection:Class:String"] != "not overrided");
        }

        [Test]
        public void Configuration_Service_Is_Requiered_Test()
        {
            Assert.Throws<Newtonsoft.Json.JsonSerializationException>(() =>
            {
                new JsonConfigurationService().ToClass<ConfigurationSample>("emptyConfigurationSample");
            });
        }

        [Test]
        public void Configuration_Manager_Test()
        {
            Assert.DoesNotThrow(() =>
            {
                var configurationManager = new JsonConfigurationManager(
                    new ConfigurationManagerOptions("configurationSample"),
                    new ConfigurationManagerOptions("configurationSample")
                    {
                        SubSections = new[] { "SubSection", "Class" },
                        ConfigurationKey = "Sample With Sub Sections"
                    },
                    new ConfigurationManagerOptions("notMergedConfigurationSample")
                    {
                        MergeConfigurationFiles = false
                    },
                    new ConfigurationManagerOptions("notMergedConfigurationSample")
                    {
                        MergeConfigurationFiles = false,
                        ConfigurationKey = "Not Merged And Force Development Environment",
                        EnvironmentDirectory = "Development"
                    });

                
                var configurationSample = configurationManager.GetConfigurationClass<ConfigurationSample>("configurationSample");

                Assert.AreEqual("overrided", configurationSample.OverrideField);

                var subSectionConfiguration = configurationManager.GetConfigurationClass<Class>("Sample With Sub Sections");

                Assert.AreEqual("String", subSectionConfiguration.String);

                var notMergedConfiguration = configurationManager.GetConfigurationClass<NotMergedConfigurationSample>("notMergedConfigurationSample");

                Assert.AreEqual("not overrided", notMergedConfiguration.OverrideField);

                var notMergedAndForcedEnvironmentConfiguration = configurationManager.GetConfigurationClass<NotMergedConfigurationSample>("Not Merged And Force Development Environment");

                Assert.AreEqual("forced environment", notMergedAndForcedEnvironmentConfiguration.OverrideField);
            });
        }
    }
}