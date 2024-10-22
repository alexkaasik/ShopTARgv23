using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ShopTARgv23.ApplicationService.Services;
using ShopTARgv23.Core.Domain;
using ShopTARgv23.Core.Dto;
using ShopTARgv23.Core.ServiceInterface;
using System;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
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

            var RealEstate1 = await Svc<IRealEstateServices>().Create(realEstate);
            var RealEstate2 = await Svc<IRealEstateServices>().Create(realEstate);

            var result = await Svc<IRealEstateServices>().Delete((Guid)RealEstate1.Id);

            Assert.NotEqual(result.Id, RealEstate2.Id);
        }

        [Fact]
        public async Task ShouldNot_DeleteByIdRealEstate_WhereDeleteRealEstate()
        {
            RealEstateDto realEstate = MockRealEstateData();

            var addRealEstate = await Svc<IRealEstateServices>().Create(realEstate);

            var result = await Svc<IRealEstateServices>().Delete((Guid)addRealEstate.Id);

            Assert.False(string.IsNullOrEmpty(result.Location));
        }

        [Fact]
        public async Task Should_UpdataRealEstate_WhenUpdateData()
        {

            var guid = Guid.Parse("82c6af77-2c7d-4eae-8e1e-29269c3c9862");
            RealEstateDto dto = MockRealEstateData();

            RealEstate domain = new();

            domain.Id = Guid.Parse("82c6af77-2c7d-4eae-8e1e-29269c3c9862");
            domain.Location = "adsg";
            domain.RoomNumber = 1;
            domain.BuildingType = "ashd";
            domain.CreatedAt = DateTime.UtcNow;
            domain.ModifiedAt = DateTime.UtcNow;

            await Svc<IRealEstateServices>().Update(dto);

            Assert.Equal(domain.Id, guid);
            Assert.DoesNotMatch(domain.Location, dto.Location);
            Assert.DoesNotMatch(domain.RoomNumber.ToString(), dto.RoomNumber.ToString());
            Assert.NotEqual(domain.Size, dto.Size);
            Assert.DoesNotMatch(domain.BuildingType, dto.BuildingType);
            
        }
        
        [Fact]
        public async Task Should_UpdataRealEstate_WhenUpdateDataVersion2()
        {
            RealEstateDto dto = MockRealEstateData();
            var CreateRealEstate = await Svc< IRealEstateServices >().Create(dto);

            RealEstateDto update = MockUpdateRealEstateData();

            var result = await Svc<IRealEstateServices>().Update(update);

            
            Assert.DoesNotMatch(CreateRealEstate.Location, result.Location);
            Assert.NotEqual(CreateRealEstate.ModifiedAt, result.ModifiedAt);
            Assert.DoesNotMatch(CreateRealEstate.RoomNumber.ToString(), result.RoomNumber.ToString());
            Assert.NotEqual(CreateRealEstate.Size, result.Size);
            Assert.DoesNotMatch(CreateRealEstate.BuildingType, result.BuildingType);

        }

        [Fact]
        public async Task ShouldNot_UpdataRealEstate_WhenUpdateData() 
        {
            RealEstateDto dto = MockRealEstateData();
            var CreateRealEstate = await Svc<IRealEstateServices>().Create(dto);

            RealEstateDto NullUpdate = MockNullRealEstateData();
            var result = await Svc<IRealEstateServices>().Create(NullUpdate);

            var NullId = NullUpdate.Id;

            Assert.True(dto.Id == NullId);
        }

        [Fact]
        public async Task ShouldNot_UpdataRealEstate_WhenNotUpdateData()
        {
            RealEstateDto dto = MockRealEstateData();
            var CreateRealEstate = await Svc<IRealEstateServices>().Create(dto);

            RealEstateDto update = MockNullRealEstateData();
            var result = await Svc<IRealEstateServices>().Create(update);

            Assert.NotEqual(result.Id, dto.Id);
        }

        /*
         * Mõtelge ise välja üks test
         * võib teha meeskonnatööd
         */

        [Fact]
        public async Task Should_ReturnNull_WhenRealEstateDoesNotExist()
        {
            Guid NonExistingId = Guid.NewGuid();

            var result = await Svc<IRealEstateServices>().DetailsAsync(NonExistingId);

            Assert.Null(result);
        }

        public async Task Should_CreateDataEntries()
        {
            RealEstateDto mock = MockRealEstateData();
            var CreateRealEstate = await Svc<IRealEstateServices>().Create(mock);

            Assert.NotNull(CreateRealEstate.Id);

            Assert.NotNull(CreateRealEstate.CreatedAt);
            Assert.NotNull(CreateRealEstate.ModifiedAt);
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

        private RealEstateDto MockUpdateRealEstateData()
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

        private RealEstateDto MockNullRealEstateData()
        {
            RealEstateDto NullDto = new()
            {
                Id = null,
                Location = "asd",
                Size = 100,
                RoomNumber = 1,
                BuildingType = "af",
                CreatedAt = DateTime.Now,
                ModifiedAt = DateTime.Now,
            };

            return NullDto;

        }
    }
}