using System;
using System.ComponentModel;
using UnityEngine;

public delegate void SROptionsPropertyChanged(object sender, string propertyName);

public partial class SROptions : INotifyPropertyChanged
{
    private static SROptions _current;

    public static SROptions Current
    {
        get
        {
            if (_current == null)
            {
                _current = CreateInstanceDelegate == null ? new SROptions() : CreateInstanceDelegate.Invoke();
            }

            return _current;
        }

    }

    public static Func<SROptions> CreateInstanceDelegate;
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    public static void OnStartup()
    {
        SRDebug.Instance.AddOptionContainer(Current);
    }

    public event SROptionsPropertyChanged PropertyChanged;

#if UNITY_EDITOR
    [JetBrains.Annotations.NotifyPropertyChangedInvocator]
#endif
    public void OnPropertyChanged(string propertyName)
    {
        if (PropertyChanged != null)
        {
            PropertyChanged(this, propertyName);
        }

        if (InterfacePropertyChangedEventHandler != null)
        {
            InterfacePropertyChangedEventHandler(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    private event PropertyChangedEventHandler InterfacePropertyChangedEventHandler;

    event PropertyChangedEventHandler INotifyPropertyChanged.PropertyChanged
    {
        add { InterfacePropertyChangedEventHandler += value; }
        remove { InterfacePropertyChangedEventHandler -= value; }
    }
}
