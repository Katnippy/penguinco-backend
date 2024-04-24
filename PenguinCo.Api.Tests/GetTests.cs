//using Microsoft.AspNetCore.Http.HttpResults;
//using PenguinCo.Api.DTOs;
//using PenguinCo.Api.Endpoints;

//namespace PenguinCo.Api.Tests;

//public class GetTests
//{
//    [Fact]
//    public void GetAllStoresReadsAllStores()
//    {
//        // Arrange

//        // Act
//        var result = StoresEndpoints.GetAllStores();

//        // Assert
//        Assert.IsType<Ok<List<StoreDto>>>(result);
//    }

//    [Fact]
//    public void GetStoreByIdReadsMatchingStore()
//    {
//        // Arrange
//        var storeToGet = 2;

//        // Act
//        var result = StoresEndpoints.GetStoreById(storeToGet);

//        // Assert
//        Assert.IsType<Ok<StoreDto>>(result.Result);
//    }

//    [Fact]
//    public void GetStoreByNonexistentIdReturns404()
//    {
//        // Arrange
//        var storeToGet = 4;

//        // Act
//        var result = StoresEndpoints.GetStoreById(storeToGet);

//        // Assert
//        Assert.IsType<NotFound>(result.Result);
//    }
//}
