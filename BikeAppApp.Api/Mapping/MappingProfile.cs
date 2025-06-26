using AutoMapper;
using BikeAppApp.Models;
using BikeAppApp.Shared.Dtos;

namespace BikeAppApp.Api.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Bayiler
            CreateMap<Bayiler, BayilerDto>().ReverseMap();
            CreateMap<Bayiler, BayilerCreateDto>().ReverseMap();
            CreateMap<Bayiler, BayilerUpdateDto>().ReverseMap();

            // BayiMotosiklet
            CreateMap<BayiMotosiklet, BayiMotosikletDto>().ReverseMap();
            CreateMap<BayiMotosiklet, BayiMotosikletCreateDto>().ReverseMap();
            CreateMap<BayiMotosiklet, BayiMotosikletUpdateDto>().ReverseMap();

            // Calisanlar
            CreateMap<Calisanlar, CalisanlarDto>().ReverseMap();
            CreateMap<Calisanlar, CalisanlarCreateDto>().ReverseMap();
            CreateMap<Calisanlar, CalisanlarUpdateDto>().ReverseMap();

            // Motosiklet
            CreateMap<Motosikletler, MotosikletDto>().ReverseMap();
            CreateMap<Motosikletler, MotosikletCreateDto>().ReverseMap();
            CreateMap<Motosikletler, MotosikletUpdateDto>().ReverseMap();

            // YetkiliServis
            CreateMap<YetkiliServis, YetkiliServisDto>().ReverseMap();
            CreateMap<YetkiliServis, YetkiliServisCreateDto>().ReverseMap();
            CreateMap<YetkiliServis, YetkiliServisUpdateDto>().ReverseMap();

            // Alicilar
            CreateMap<Alicilar, AlicilarDto>().ReverseMap();
            CreateMap<Alicilar, AlicilarCreateDto>().ReverseMap();
            CreateMap<Alicilar, AlicilarUpdateDto>().ReverseMap();

            // BakimGecmisi
            CreateMap<BakimGecmisi, BakimGecmisiDto>().ReverseMap();
            CreateMap<BakimGecmisi, BakimGecmisiCreateDto>().ReverseMap();
            CreateMap<BakimGecmisi, BakimGecmisiUpdateDto>().ReverseMap();

            CreateMap<BayiAlici, BayiAliciDto>().ReverseMap();

        }
    }
}
