// using System.Net;
// using GeoLocal.DataAccessLayer.Entities;
// using GeoLocal.DataAccessLayer.Repository;
// using GeoLocal.Interfaces;
// using GeoLocal.Models;
// using GeoLocal.Responses;
//
// namespace GeoLocal.Services;
//
// public class CalculationService : ICalculationService
// {
//     private readonly IAllegroApiClient _allegroApiClient;
//     private readonly IIpStackUrlBuilder _ipStackUrlBuilder;
//     private readonly IGeoLocalDbRepository _geoLocalDbRepository;
//     
//     public CalculationService(IAllegroApiClient allegroApiClient, IIpStackUrlBuilder ipStackUrlBuilder, IGeoLocalDbRepository geoLocalDbRepository)
//     {
//         _allegroApiClient = allegroApiClient;
//         _ipStackUrlBuilder = ipStackUrlBuilder;
//         _geoLocalDbRepository = geoLocalDbRepository;
//     }
//     
//     #region Public Methods 
//     
//     public async Task<ServiceResponse<OfferFeeDto>> GetCalculatedTotalOfferFeeByIdAsync(string offerId)
//     {
//         var accessToken = await _ipStackUrlBuilder.GetAccessForUserTokenAsync();
//         var billingResponse = await _allegroApiClient.GetBillingByOfferIdAsync(offerId,$"Bearer {accessToken}");
//         
//         if (billingResponse.StatusCode != HttpStatusCode.OK)
//         {
//             return new ServiceResponse<OfferFeeDto>(billingResponse.Error.Message, ServiceStatusCodes.StatusCode.Error);
//         }
//
//         if (billingResponse.Content == null)
//         {
//             return new ServiceResponse<OfferFeeDto>($"Billing entries not found for the offer id {offerId}.", ServiceStatusCodes.StatusCode.NotFound);
//         }
//         
//         try
//         {
//             var result = new OfferFeeDto();
//             result.OfferId = offerId;
//             List<BillingEntry> billingEntries = billingResponse.Content.BillingEntries;
//             var billingSum = billingEntries.Sum(billingEntry => billingEntry.Value.Amount);
//             
//             var totalSaleResponse = await GetCalculatedTotalOfferSaleAsync(offerId, GetListOfUniqueOrderDataId(billingEntries));
//             if (totalSaleResponse.ResponseStatus != ServiceStatusCodes.StatusCode.Success)
//                 return new ServiceResponse<OfferFeeDto>(totalSaleResponse.Message, ServiceStatusCodes.StatusCode.Error);
//             
//             if (totalSaleResponse.Data == 0)
//                 return new ServiceResponse<OfferFeeDto>($"Total sale amount for the offer id {offerId} is 0.", ServiceStatusCodes.StatusCode.Error);
//             
//             result.FeePercent = Math.Abs(billingSum / totalSaleResponse.Data);
//
//
//             var offerFee = new OfferFee(result.OfferId, result.FeePercent);
//             _mfcDbRepository.AddOfferFee(offerFee);
//             await _mfcDbRepository.SaveChangesAsync();
//             
//             return new ServiceResponse<OfferFeeDto>(result);
//         }
//         catch (Exception e)
//         {
//             return new ServiceResponse<OfferFeeDto>(e.Message, ServiceStatusCodes.StatusCode.Error);
//         }
//     }
//
//     #endregion
//     
//     #region Private Methods   
//     
//     
//     #endregion
// }