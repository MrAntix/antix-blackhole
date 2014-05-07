antix-blackhole
===============

Too simple template engine 

Simple placeholder
------------------

    "Dear {{Name}}"         + {{"Name", "Bob"}}       => "Dear Bob"
    
Placeholder with suffix or prefix
---------------------------------

if Name is null suffix and prefix not rendered

    "{Dear {Name}, }"       + {{"Name", "Bob"}}       => "Dear Bob, "
    "{Dear {Name}, }"       + {{"Name", null}}        => ""
  
Placeholder with default
------------------------

if Name is null default is used

    "{Dear {Name|Sir}, }"   + {{"Name", "Bob"}}       => "Dear Bob, "
    "{Dear {Name|Sir}, }"   + {{"Name", null}}        => "Dear Sir, "
    
    
That is all, hope you didn't get too excited
