EF6Profiler
===========

This project allows you to profile Entity Framework 6 by logging or streaming commands as they are being run.

By referencing this project and adding a couple of lines you can profile all database calls going through EF6. If you opt to use the SignalR profiler you can run the ProfilerClient WPF app in this repository to see a realtime profile of DbCommands as they run. To see this in action run both the EFSample and ProfileClient projects. 

This can be extended to log/stream to other places by implementing your own IProfileLogger and passing that logger into the interceptor e.g

    using (var profiler = new SignalRLogger())
    {
      DbInterception.Add(new CommandInterceptor(profiler));
      //EF code here...
    }

