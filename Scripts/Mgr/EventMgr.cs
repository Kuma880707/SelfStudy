using System.Collections.Generic;

public class EventMgr : UnitySingleton<EventMgr>
{
    public delegate void event_handler(string event_name, object udata);
    private Dictionary<string, event_handler> dic = new Dictionary<string, event_handler>();
    public void add_listener(string event_name, event_handler h)
    {
        if(dic.ContainsKey(event_name))
        {
            this.dic[event_name] += h;
        }
        else
        {
            this.dic.Add(event_name, h);
        }
    }
    public void remove_listener(string event_name, event_handler h)
    {
        if (!dic.ContainsKey(event_name))
        {
            return;
        }
        this.dic[event_name] -= h;
        if(this.dic[event_name] == null)
        {
            this.dic.Remove(event_name);
        }
    }

    public void dispatch_event(string event_name, object udata)
    {
        if (!dic.ContainsKey(event_name))
        {
            return;
        }
        this.dic[event_name](event_name, udata);
    }

}
