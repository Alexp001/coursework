# [SobaScript.Z.VS](https://github.com/3F/SobaScript.Z.VS)

Components for work with Visual Studio through SobaScript. Extensible Modular Scripting Programming Language.

**-- #SobaScript**

https://github.com/3F/SobaScript

[![Build status](https://ci.appveyor.com/api/projects/status/r6q9648i8nrgec4l/branch/master?svg=true)](https://ci.appveyor.com/project/3Fs/sobascript-z-vs/branch/master)
[![release-src](https://img.shields.io/github/release/3F/SobaScript.Z.VS.svg)](https://github.com/3F/SobaScript.Z.VS/releases/latest)
[![License](https://img.shields.io/badge/License-MIT-74A5C2.svg)](https://github.com/3F/SobaScript.Z.VS/blob/master/License.txt)
[![NuGet package](https://img.shields.io/nuget/v/SobaScript.Z.VS.svg)](https://www.nuget.org/packages/SobaScript.Z.VS/)
[![Tests](https://img.shields.io/appveyor/tests/3Fs/sobascript-z-vs/master.svg)](https://ci.appveyor.com/project/3Fs/sobascript-z-vs/build/tests)

[![Build history](https://buildstats.info/appveyor/chart/3Fs/sobascript-z-vs?buildCount=20&showStats=true)](https://ci.appveyor.com/project/3Fs/sobascript-z-vs/history)

## License

Licensed under the [MIT License](https://github.com/3F/SobaScript.Z.VS/blob/master/License.txt)

```
Copyright (c) 2014-2019  Denis Kuzmin < x-3F@outlook.com > GitHub/3F
```

[ [ ☕ Donate ](https://3F.github.com/Donation/) ]

SobaScript.Z.VS contributors: https://github.com/3F/SobaScript.Z.VS/graphs/contributors

## Provides at least the following

### DTEComponent

For work with [EnvDTE](http://msdn.microsoft.com/en-us/library/EnvDTE.aspx)

```clojure
#[DTE exec: Build.SolutionPlatforms(x86)] 
#[DTE exec: Build.SolutionConfigurations(Debug_Exclude_Plugins_All)]
#[DTE exec: Build.Cancel]
...
```

```clojure
string #[DTE events.LastCommand.Guid]
object #[DTE events.LastCommand.CustomOut]
object #[DTE events.LastCommand.CustomIn]
```

### OwpComponent

For work with Output Window Pane.

```clojure
#[OWP item("My Item").activate = true]
#[OWP item("My Item").write(true): mixed data]
#[OWP item("My Item").clear = true]
#[OWP item("My Item").delete = true]
```

```clojure
string #[OWP out.Warnings]
integer #[OWP out.Warnings.Count]

List #[OWP out.Warnings.Codes]
C4702,4505,..
```

```clojure
string #[OWP out.Errors]
Integer #[OWP out.Errors.Count]

List #[OWP out.Errors.Codes]
C4702,C4505,..
```

### BuildComponent 

Managing of build process at runtime.


```clojure
#[(#[Build projects.find("ZenLib").IsBuildable]) {
    ...
}]

#[Build projects.find("ZenLib").IsBuildable = false]
```

```clojure
#[(#[Build projects.find("ZenLib").IsDeployable]) {
    ...
}]

#[Build projects.find("ZenLib").IsDeployable= false]
```

```clojure
enum #[Build type]
void cancel = boolean
```

```clojure
#[Build solution.current.First.path]
#[Build solution.path("D:\tmp\app.sln").First.guid]
```

```clojure
List #[Build solution.path(string sln).GuidList]
{73919171-44B6-4536-B892-F1FCA653887C},{4262A1DC-768F-43CC-85F5-A4ED9CD034CC},
{A7BF1F9C-F18D-423E-9354-859DC3CFAFD4}, ...
```

```clojure
#[Build solution.path("D:\tmp\vsSolutionBuildEvent.sln")
        .projectBy("{97F0E2FF-42DB-4506-856D-8694DD99F827}").name]
```