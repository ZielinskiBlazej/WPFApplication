using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace WPFApp.Models;

public partial class Door : INotifyPropertyChanged
{
    public int Id { get; set; }

    public string? dname;
    public string? dmodel;
    public decimal? dprice;
    public string? Dname
    {
        get
        {
            return dname;
        }
        set
        {
            dname = value;
            OnPropertyChanged(nameof(dname));
        }
    }

    public string? Dmodel
    {
        get
        {
            return dmodel;
        }
        set
        {
            dmodel = value;
            OnPropertyChanged(nameof (dmodel));
        }
    }

    public decimal? Dprice
    {
        get
        {
            return dprice;
        }
        set
        {
            dprice = value;
            OnPropertyChanged($"{nameof (dprice)}");
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
