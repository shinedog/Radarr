﻿using Nancy;
using NzbDrone.Core.Providers;
using NzbDrone.Api.QualityType;
using NzbDrone.Core.Repository;

namespace NzbDrone.Api.QualityProfiles
{
    public class RootFolderModule : NzbDroneApiModule
    {
        private readonly RootDirProvider _rootDirProvider;

        public RootFolderModule(RootDirProvider rootDirProvider)
            : base("//rootfolders")
        {
            _rootDirProvider = rootDirProvider;

            Get["/"] = x => GetRootFolders();
            Post["/"] = x => AddRootFolder();
        }

        private Response AddRootFolder()
        {
            _rootDirProvider.Add(Request.Body.FromJson<RootDir>());
            return new Response { StatusCode = HttpStatusCode.Created };
        }

        private Response GetRootFolders()
        {
            return _rootDirProvider.AllWithFreeSpace().AsResponse();
        }
    }
}