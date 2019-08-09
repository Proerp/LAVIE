using System;
using System.Collections.Generic;

using AutoMapper;

using TotalBase;
using TotalModel.Models;
using TotalDTO.Productions;
using TotalCore.Repositories.Productions;

namespace TotalSmartCoding.Controllers.APIs.Productions
{
    public class ScannerAPIs
    {
        private readonly IPackRepository packRepository;
        private readonly ICartonRepository cartonRepository;
        private readonly IPalletRepository palletRepository;

        public ScannerAPIs(IPackRepository packRepository, ICartonRepository cartonRepository, IPalletRepository palletRepository)
        {
            this.packRepository = packRepository;
            this.cartonRepository = cartonRepository;
            this.palletRepository = palletRepository;
        }

        public IList<BarcodeDTO> GetBarcodeList(GlobalVariables.FillingLine fillingLineID, int cartonID, int palletID, int batchID)
        {
            try
            {
                IList<BarcodeDTO> barcodeList = new List<BarcodeDTO>();

                if (cartonID > 0)
                {
                    IList<Pack> packs = this.packRepository.GetPacks(fillingLineID, (int)GlobalVariables.BarcodeStatus.Freshnew + "," + (int)GlobalVariables.BarcodeStatus.Readytoset + "," + (int)GlobalVariables.BarcodeStatus.Wrapped, cartonID);
                    if (packs.Count > 0)
                    {
                        packs.Each(pack =>
                        {
                            PackDTO packDTO = Mapper.Map<Pack, PackDTO>(pack);
                            barcodeList.Add(packDTO);
                        });
                    }
                }
                else
                    if (palletID > 0)
                    {
                        IList<Carton> cartons = this.cartonRepository.GetCartons(fillingLineID, (int)GlobalVariables.BarcodeStatus.Freshnew + "," + (int)GlobalVariables.BarcodeStatus.Readytoset + "," + (int)GlobalVariables.BarcodeStatus.Wrapped + "," + (int)GlobalVariables.BarcodeStatus.Pending + "," + (int)GlobalVariables.BarcodeStatus.Noread, palletID);
                        if (cartons.Count > 0)
                        {
                            cartons.Each(carton =>
                            {
                                CartonDTO cartonDTO = Mapper.Map<Carton, CartonDTO>(carton);
                                barcodeList.Add(cartonDTO);
                            });
                        }
                    }
                    else
                        if (batchID > 0)
                        {
                            IList<Pallet> pallets = this.palletRepository.GetPallets(fillingLineID, batchID, (int)GlobalVariables.BarcodeStatus.Freshnew + "," + (int)GlobalVariables.BarcodeStatus.Readytoset + "," + (int)GlobalVariables.BarcodeStatus.Wrapped);
                            if (pallets.Count > 0)
                            {
                                pallets.Each(pallet =>
                                {
                                    PalletDTO palletDTO = Mapper.Map<Pallet, PalletDTO>(pallet);
                                    barcodeList.Add(palletDTO);
                                });
                            }
                        }

                return barcodeList;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }










        public IList<PackDTO> SearchPacks(string barcode)
        {
            try
            {
                IList<PackDTO> packDTOs = new List<PackDTO>();

                IList<Pack> packs = this.packRepository.SearchPacks(barcode);
                Mapper.Map<IList<Pack>, IList<PackDTO>>(packs, packDTOs);

                return packDTOs;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public IList<CartonDTO> SearchCartons(string barcode)
        {
            try
            {
                IList<CartonDTO> cartonDTOs = new List<CartonDTO>();

                IList<Carton> cartons = this.cartonRepository.SearchCartons(barcode);
                Mapper.Map<IList<Carton>, IList<CartonDTO>>(cartons, cartonDTOs);

                return cartonDTOs;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }


        public IList<PalletDTO> SearchPallets(string barcode)
        {
            try
            {
                IList<PalletDTO> palletDTOs = new List<PalletDTO>();

                IList<Pallet> pallets = this.palletRepository.SearchPallets(barcode);
                Mapper.Map<IList<Pallet>, IList<PalletDTO>>(pallets, palletDTOs);

                return palletDTOs;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
    }
}
