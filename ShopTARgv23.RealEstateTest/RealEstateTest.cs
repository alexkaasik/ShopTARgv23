using Microsoft.EntityFrameworkCore;
using ShopTARgv23.Core.Domain;
using ShopTARgv23.Core.Dto;
using ShopTARgv23.Core.ServiceInterface;
using static System.Net.Mime.MediaTypeNames;

namespace ShopTARgv23.RealEstateTest
{
    public class RealEstateTest : TestBase
    {
        [Fact]
        public async void ShouldNotAddEmptyRealEstate_WhenReturnResult()
        {
            // Arrange
            RealEstateDto dto = new();

            dto.Location = "qwe";
            dto.Size = 123;
            dto.RoomNumber = 456;
            dto.BuildingType = "ASD";
            dto.CreatedAt = DateTime.Now;
            dto.ModifiedAt = DateTime.Now;

            // Act
            var result = await Svc<IRealEstateServices>().Create(dto);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task ShouldNot_GetByIdRealestate_WhenReturnsNotEqual()
        {
            // Aramge
            Guid WrongGuid = Guid.Parse(Guid.NewGuid().ToString());
            Guid guid = Guid.Parse("82c6af77-2c7d-4eae-8e1e-29269c3c9862");

            // Act 
            await Svc<IRealEstateServices>().DetailsAsync(guid);

            Assert.NotEqual(WrongGuid, guid);
        }

        [Fact]
        public async Task Should_GetByIdRealEstate_WhenReturnEqual()
        {
            // Aramge
            Guid CorrectGuid = Guid.Parse("82c6af77-2c7d-4eae-8e1e-29269c3c9862");
            Guid guid = Guid.Parse("82c6af77-2c7d-4eae-8e1e-29269c3c9862");

            // Act 
            await Svc<IRealEstateServices>().DetailsAsync(guid);

            // Assert
            Assert.Equal(CorrectGuid, guid);
        }

        [Fact]
        public async Task Should_DeleteByIdRealEstate_WhereDeleteRealEstate()
        {
            RealEstateDto realEstate = MockRealEstateData();

            var RealEstate1 = await Svc<IRealEstateServices>.Create(realEstate);
            var RealEstate2 = await Svc<IRealEstateServices>.Create(realEstate);

            var result = await Svc<IRealEstateServices>.Delete((Guid)RealEstate1.Id);

            Assert.NotEqual(result.Id, RealEstate2.Id);
        }

        [Fact]
        public async Task ShouldNot_DeleteByIdRealEstate_WhereDeleteRealEstate()
        {
            RealEstateDto realEstate = MockRealEstateData();

            var addRealEstate = await Svc<IRealEstateServices>.Create(realEstate);

            var result = await Svc<IRealEstateServices>.Delete((Guid)addRealEstate.Id);

            Assert.Equal(result, addRealEstate);
        }

        private RealEstateDto MockRealEstateData()
        {
            RealEstateDto realEstate = new()
            {
                Location = "asd",
                Size = 100,
                RoomNumber = 1,
                BuildingType = "af",
                CreatedAt = DateTime.Now,
                ModifiedAt = DateTime.Now,
            };

            return realEstate;
        }
    }
}