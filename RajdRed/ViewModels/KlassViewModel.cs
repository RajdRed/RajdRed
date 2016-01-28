﻿using RajdRed.Models;
using RajdRed.Repositories;
using RajdRed.ViewModels.Commands;
using RajdRed.Views;

namespace RajdRed.ViewModels
{
    public class KlassViewModel
    {   
        public KlassModel KlassModel { get; set; }
        public KlassView KlassView { get; set; }
        public KlassRepository KlassRepository { get; set; }
        public NodKlassRepository NodKlassRepository { get; set; }
        public KlassViewModel(KlassModel km, KlassRepository kr)
        {
            KlassModel = km;
            KlassRepository = kr;
            NodKlassRepository = new NodKlassRepository(this);
        }

        public KlassViewModel(KlassModel km)
        {
            KlassModel = km;
        }

        public KlassViewModel(){}

        public void Delete()
        {
            KlassRepository.Remove(this);
        }

        public void SetKlassView(KlassView kv)
        {
            KlassView = kv;
        }

    }
}