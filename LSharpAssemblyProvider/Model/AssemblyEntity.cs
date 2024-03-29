﻿using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using GalaSoft.MvvmLight;
using LSharpAssemblyProvider.Helpers;
using LSharpAssemblyProvider.Properties;
using Newtonsoft.Json;

namespace LSharpAssemblyProvider.Model
{
    public class AssemblyEntity : ObservableObject
    {
        public string Name { get; set; }

        [JsonIgnore]
        public string Repositroy
        {
            get
            {
                var match = Regex.Match(Url, @"(?i:https://)(?<server>[^\s/]*)/(?<user>[^\s/]*)/(?<repo>[^\s/]*)");
                if (!match.Success && match.Groups["server"].Value == "github.com")
                    return "Invalid Github URL";

                return match.Groups["repo"].Value;
            }
        }
        [JsonIgnore]
        public string Developer
        {
            get
            {
                var match = Regex.Match(Url, @"(?i:https://)(?<server>[^\s/]*)/(?<user>[^\s/]*)/(?<repo>[^\s/]*)");
                if (!match.Success && match.Groups["server"].Value == "github.com")
                    return "Invalid Github URL";

                return match.Groups["user"].Value;
            }
        }
        public string Url { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        [JsonIgnore]
        public long RepositroyVersion
        {
            get
            {
                return _repositroyVersion;
            }
            set
            {
                Set(() => RepositroyVersion, ref _repositroyVersion, value);
            }
        }
        [JsonIgnore]
        public long LocalVersion
        {
            get
            {
                return _localVersion;
            }
            set
            {
                Set(() => LocalVersion, ref _localVersion, value);
            }
        }
        public int Points { get; set; }
        public int Votes { get; set; }
        [JsonIgnore]
        public string State
        {
            get
            {
                return _state;
            }
            set
            {
                Set(() => State, ref _state, value);
            }
        }
        [JsonIgnore]
        public string Rating
        {
            get
            {
                return Votes > 0 ? "/Images/star" + (Points/Votes) + ".png" : "/Images/star0.png";
            }
        }


        private string _state;
        private long _repositroyVersion;
        private long _localVersion;

        public AssemblyEntity()
        {
        }

        public AssemblyEntity(string name, string url)
        {
            Name = name;
            Url = url;
        }

        public ProjectFile GetProjectFile()
        {
            var file = Directory.GetFiles(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Repositories", Developer, Repositroy), Name + ".csproj", SearchOption.AllDirectories).FirstOrDefault();

            return new ProjectFile(file)
            {
                Configuration = "Release",
                PlatformTarget = "x86",
                ReferencesPath = Path.Combine(Settings.Default.LeagueSharpPath, "Assemblies", "System"),
                UpdateReferences = true,
                PostbuildEvent = true,
                PrebuildEvent = true
            };
        }
    }
}
