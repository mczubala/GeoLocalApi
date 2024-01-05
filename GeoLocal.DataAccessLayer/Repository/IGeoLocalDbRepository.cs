using GeoLocal.DataAccessLayer.Entities;

namespace GeoLocal.DataAccessLayer.Repository;

public interface IGeoLocalDbRepository
{
    Task<List<OfferFee>> GetOfferFeesByOfferIdAsync(string offerId);
    Task<bool> SaveChangesAsync();
    Task AddOfferFee(OfferFee newOfferFee);
    Task AddAllegroAccessToken(AllegroAccessToken newAllegroAccessToken);
    Task<AllegroAccessToken> GetAllegroAccessTokenByClientIdAsync(string clientId);
    Task UpdateAllegroAccessTokenAsync(AllegroAccessToken newAllegroAccessToken);
}