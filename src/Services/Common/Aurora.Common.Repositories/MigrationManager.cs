using Aurora.Common.Domain.Catalogs.Models;
using Aurora.Common.Domain.Locations.Models;
using Aurora.Common.Domain.Settings.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Aurora.Common.Repositories
{
    public static class MigrationManager
    {
        public static IHost MigrateDatabase(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                using (var context = scope.ServiceProvider.GetRequiredService<CommonDataContext>())
                {
                    CreateGlobalCatalog(context);
                    CreateSecurityAttributes(context);
                    CreateCountries(context);
                }
            }

            return host;
        }

        #region Métodos de carga inicial de registros

        private static void CreateGlobalCatalog(CommonDataContext context)
        {
            // Creación de catálogo 'TipoAtributo'
            var catalog = context
                .Catalogs
                .FirstOrDefault(x => x.Code.Equals("TipoAtributo"));

            if (catalog != null) return;

            context.Catalogs.Add(new CatalogData()
            {
                Code = "TipoAtributo",
                Name = "Tipo de Atributo",
                Description = "Tipo, nivel o ámbito de configuración de atributos.",
                IsVisible = true,
                IsEditable = false,
                Items = new List<CatalogItemData>
                {
                    new CatalogItemData()
                    {
                        Code = "Security",
                        Description = "Atributos de Seguridad General.",
                        IsEditable = false,
                        IsActive = true,
                        CreatedBy = "BATCH-USR",
                        CreatedDate = DateTime.Now,
                        LastUpdatedBy = "BATCH-USR",
                        LastUpdatedDate = DateTime.Now
                    }
                }
            });

            context.SaveChanges();
        }

        private static void CreateSecurityAttributes(CommonDataContext context)
        {
            // Obtención de atributos de tipo 'Security'
            var settings = context
                .AttributeSettings
                .Where(x => x.ScopeType.Equals("Security"))
                .ToList();

            // Creación de atributo 'UserPasswordExpirationPolicy'
            if (!settings.Exists(x => x.Code.Equals("UserPasswordExpirationPolicy")))
            {
                context.AttributeSettings.Add(new AttributeSettingData()
                {
                    Code = "UserPasswordExpirationPolicy",
                    Name = "Política de expiración de contraseñas",
                    Description = "Indica si se encuentra habilitada la política de expiración de contraseñas.",
                    ScopeType = "Security",
                    DataType = "Boolean",
                    Configuration = "<booleanSetting><defaultValue>true</defaultValue></booleanSetting>",
                    IsVisible = true,
                    IsEditable = false,
                    IsActive = true
                });

                context.SaveChanges();
            }

            // Creación de atributo 'UserPasswordExpirationDays'
            if (!settings.Exists(x => x.Code.Equals("UserPasswordExpirationDays")))
            {
                context.AttributeSettings.Add(new AttributeSettingData()
                {
                    Code = "UserPasswordExpirationDays",
                    Name = "Días de validez de contraseñas",
                    Description = "Indica el número máximo de días que tendrá validez la contraseña cuando se modifica por un usuario.",
                    ScopeType = "Security",
                    DataType = "Integer",
                    Configuration = "<integerSetting><minValue>1</minValue><maxValue>365</maxValue><defaultValue>365</defaultValue></integerSetting>",
                    IsVisible = true,
                    IsEditable = true,
                    IsActive = true
                });

                context.SaveChanges();
            }

            // Creación de atributo 'UserPasswordPatternPolicy'
            if (!settings.Exists(x => x.Code.Equals("UserPasswordPatternPolicy")))
            {
                context.AttributeSettings.Add(new AttributeSettingData()
                {
                    Code = "UserPasswordPatternPolicy",
                    Name = "Patrón de generación de contraseñas",
                    Description = "Establece el patrón a validar en la creación de contraseñas (si está en blanco no se aplica).",
                    ScopeType = "Security",
                    DataType = "Text",
                    Configuration = "<textSetting><minLength>0</minLength><maxLength>200</maxLength><defaultValue /><pattern /></textSetting>",
                    IsVisible = true,
                    IsEditable = true,
                    IsActive = true
                });

                context.SaveChanges();
            }

            // Creación de atributo 'UserPasswordHistoryValidationCount'
            if (!settings.Exists(x => x.Code.Equals("UserPasswordHistoryValidationCount")))
            {
                context.AttributeSettings.Add(new AttributeSettingData()
                {
                    Code = "UserPasswordHistoryValidationCount",
                    Name = "Número de contraseñas históricas",
                    Description = "Indica la cantidad de las últimas contraseñas ingresadas que se deben validar cuando un usuario modifica su contraseña. Si el valor es cero, no se aplica la validación.",
                    ScopeType = "Security",
                    DataType = "Integer",
                    Configuration = "<integerSetting><minValue>1</minValue><maxValue>100</maxValue><defaultValue>1</defaultValue></integerSetting>",
                    IsVisible = true,
                    IsEditable = true,
                    IsActive = true
                });

                context.SaveChanges();
            }
        }

        private static void CreateCountries(CommonDataContext context)
        {
            if (context.Countries.Any()) return;

            context.Countries.AddRange(
                new CountryData() { Name = "Afganistán", OfficialName = "República Islámica de Afganistán", TwoLettersCode = "AF", ThreeLettersCode = "AFG", ThreeDigitsCode = "004", InternetPrefix = ".af", IsActive = true },
                new CountryData() { Name = "Åland", OfficialName = "Islas Åland", TwoLettersCode = "AX", ThreeLettersCode = "ALA", ThreeDigitsCode = "248", InternetPrefix = ".ax", IsActive = true },
                new CountryData() { Name = "Albania", OfficialName = "República de Albania", TwoLettersCode = "AL", ThreeLettersCode = "ALB", ThreeDigitsCode = "008", InternetPrefix = ".al", IsActive = true },
                new CountryData() { Name = "Alemania", OfficialName = "República Federal de Alemania", TwoLettersCode = "DE", ThreeLettersCode = "DEU", ThreeDigitsCode = "276", InternetPrefix = ".de", IsActive = true },
                new CountryData() { Name = "Andorra", OfficialName = "Principado de Andorra", TwoLettersCode = "AD", ThreeLettersCode = "AND", ThreeDigitsCode = "020", InternetPrefix = ".ad", IsActive = true },
                new CountryData() { Name = "Angola", OfficialName = "República de Angola", TwoLettersCode = "AO", ThreeLettersCode = "AGO", ThreeDigitsCode = "024", InternetPrefix = ".ao", IsActive = true },
                new CountryData() { Name = "Anguila", OfficialName = "Anguila", TwoLettersCode = "AI", ThreeLettersCode = "AIA", ThreeDigitsCode = "660", InternetPrefix = ".ai", IsActive = true },
                new CountryData() { Name = "Antártida", OfficialName = "Antártida", TwoLettersCode = "AQ", ThreeLettersCode = "ATA", ThreeDigitsCode = "010", InternetPrefix = ".aq", IsActive = true },
                new CountryData() { Name = "Antigua y Barbuda", OfficialName = "Antigua y Barbuda", TwoLettersCode = "AG", ThreeLettersCode = "ATG", ThreeDigitsCode = "028", InternetPrefix = ".ag", IsActive = true },
                new CountryData() { Name = "Arabia Saudita", OfficialName = "Reino de Arabia Saudita", TwoLettersCode = "SA", ThreeLettersCode = "SAU", ThreeDigitsCode = "682", InternetPrefix = ".sa", IsActive = true },
                new CountryData() { Name = "Argelia", OfficialName = "República Democrática Popular de Argelia", TwoLettersCode = "DZ", ThreeLettersCode = "DZA", ThreeDigitsCode = "012", InternetPrefix = ".dz", IsActive = true },
                new CountryData() { Name = "Argentina", OfficialName = "República Argentina", TwoLettersCode = "AR", ThreeLettersCode = "ARG", ThreeDigitsCode = "032", InternetPrefix = ".ar", IsActive = true },
                new CountryData() { Name = "Armenia", OfficialName = "República de Armenia", TwoLettersCode = "AM", ThreeLettersCode = "ARM", ThreeDigitsCode = "051", InternetPrefix = ".am", IsActive = true },
                new CountryData() { Name = "Aruba", OfficialName = "Aruba", TwoLettersCode = "AW", ThreeLettersCode = "ABW", ThreeDigitsCode = "533", InternetPrefix = ".aw", IsActive = true },
                new CountryData() { Name = "Australia", OfficialName = "Mancomunidad de Australia", TwoLettersCode = "AU", ThreeLettersCode = "AUS", ThreeDigitsCode = "036", InternetPrefix = ".au", IsActive = true },
                new CountryData() { Name = "Austria", OfficialName = "República de Austria", TwoLettersCode = "AT", ThreeLettersCode = "AUT", ThreeDigitsCode = "040", InternetPrefix = ".at", IsActive = true },
                new CountryData() { Name = "Azerbaiyán", OfficialName = "República de Azerbaiyán", TwoLettersCode = "AZ", ThreeLettersCode = "AZE", ThreeDigitsCode = "031", InternetPrefix = ".az", IsActive = true },
                new CountryData() { Name = "Bahamas", OfficialName = "Mancomunidad de Las Bahamas", TwoLettersCode = "BS", ThreeLettersCode = "BHS", ThreeDigitsCode = "044", InternetPrefix = ".bs", IsActive = true },
                new CountryData() { Name = "Bangladés", OfficialName = "República Popular de Bangladesh", TwoLettersCode = "BD", ThreeLettersCode = "BGD", ThreeDigitsCode = "050", InternetPrefix = ".bd", IsActive = true },
                new CountryData() { Name = "Barbados", OfficialName = "Barbados", TwoLettersCode = "BB", ThreeLettersCode = "BRB", ThreeDigitsCode = "052", InternetPrefix = ".bb", IsActive = true },
                new CountryData() { Name = "Baréin", OfficialName = "Reino de Baréin", TwoLettersCode = "BH", ThreeLettersCode = "BHR", ThreeDigitsCode = "048", InternetPrefix = ".bh", IsActive = true },
                new CountryData() { Name = "Belarús", OfficialName = "República de Belarús", TwoLettersCode = "BY", ThreeLettersCode = "BLR", ThreeDigitsCode = "112", InternetPrefix = ".by", IsActive = true },
                new CountryData() { Name = "Bélgica", OfficialName = "Reino de Bélgica", TwoLettersCode = "BE", ThreeLettersCode = "BEL", ThreeDigitsCode = "056", InternetPrefix = ".be", IsActive = true },
                new CountryData() { Name = "Belice", OfficialName = "Belize", TwoLettersCode = "BZ", ThreeLettersCode = "BLZ", ThreeDigitsCode = "084", InternetPrefix = ".bz", IsActive = true },
                new CountryData() { Name = "Benín", OfficialName = "República de Benín", TwoLettersCode = "BJ", ThreeLettersCode = "BEN", ThreeDigitsCode = "204", InternetPrefix = ".bj", IsActive = true },
                new CountryData() { Name = "Bermudas", OfficialName = "Bermudas", TwoLettersCode = "BM", ThreeLettersCode = "BMU", ThreeDigitsCode = "060", InternetPrefix = ".bm", IsActive = true },
                new CountryData() { Name = "Bhután", OfficialName = "Reino de Bhután", TwoLettersCode = "BT", ThreeLettersCode = "BTN", ThreeDigitsCode = "064", InternetPrefix = ".bt", IsActive = true },
                new CountryData() { Name = "Bolivia", OfficialName = "Estado Plurinacional de Bolivia", TwoLettersCode = "BO", ThreeLettersCode = "BOL", ThreeDigitsCode = "068", InternetPrefix = ".bo", IsActive = true },
                new CountryData() { Name = "Bonaire, San Eustaquio y Saba", OfficialName = "Bonaire, San Eustaquio y Saba", TwoLettersCode = "BQ", ThreeLettersCode = "BES", ThreeDigitsCode = "535", InternetPrefix = ".bq", IsActive = true },
                new CountryData() { Name = "Bosnia y Herzegovina", OfficialName = "Bosnia y Herzegovina", TwoLettersCode = "BA", ThreeLettersCode = "BIH", ThreeDigitsCode = "070", InternetPrefix = ".ba", IsActive = true },
                new CountryData() { Name = "Botsuana", OfficialName = "República de Botswana", TwoLettersCode = "BW", ThreeLettersCode = "BWA", ThreeDigitsCode = "072", InternetPrefix = ".bw", IsActive = true },
                new CountryData() { Name = "Brasil", OfficialName = "República Federativa de Brasil", TwoLettersCode = "BR", ThreeLettersCode = "BRA", ThreeDigitsCode = "076", InternetPrefix = ".br", IsActive = true },
                new CountryData() { Name = "Brunéi", OfficialName = "Estado de Brunéi, Morada de la Paz", TwoLettersCode = "BN", ThreeLettersCode = "BRN", ThreeDigitsCode = "096", InternetPrefix = ".bn", IsActive = true },
                new CountryData() { Name = "Bulgaria", OfficialName = "República de Bulgaria", TwoLettersCode = "BG", ThreeLettersCode = "BGR", ThreeDigitsCode = "100", InternetPrefix = ".bg", IsActive = true },
                new CountryData() { Name = "Burkina Faso", OfficialName = "Burkina Faso", TwoLettersCode = "BF", ThreeLettersCode = "BFA", ThreeDigitsCode = "854", InternetPrefix = ".bf", IsActive = true },
                new CountryData() { Name = "Burundi", OfficialName = "República de Burundi", TwoLettersCode = "BI", ThreeLettersCode = "BDI", ThreeDigitsCode = "108", InternetPrefix = ".bi", IsActive = true },
                new CountryData() { Name = "Cabo Verde", OfficialName = "República de Cabo Verde", TwoLettersCode = "CV", ThreeLettersCode = "CPV", ThreeDigitsCode = "132", InternetPrefix = ".cv", IsActive = true },
                new CountryData() { Name = "Camboya", OfficialName = "Reino de Camboya", TwoLettersCode = "KH", ThreeLettersCode = "KHM", ThreeDigitsCode = "116", InternetPrefix = ".kh", IsActive = true },
                new CountryData() { Name = "Camerún", OfficialName = "República de Camerún", TwoLettersCode = "CM", ThreeLettersCode = "CMR", ThreeDigitsCode = "120", InternetPrefix = ".cm", IsActive = true },
                new CountryData() { Name = "Canadá", OfficialName = "Canadá", TwoLettersCode = "CA", ThreeLettersCode = "CAN", ThreeDigitsCode = "124", InternetPrefix = ".ca", IsActive = true },
                new CountryData() { Name = "Catar", OfficialName = "Estado de Qatar", TwoLettersCode = "QA", ThreeLettersCode = "QAT", ThreeDigitsCode = "634", InternetPrefix = ".qa", IsActive = true },
                new CountryData() { Name = "Chad", OfficialName = "República de Chad", TwoLettersCode = "TD", ThreeLettersCode = "TCD", ThreeDigitsCode = "148", InternetPrefix = ".td", IsActive = true },
                new CountryData() { Name = "Chequia", OfficialName = "República Checa", TwoLettersCode = "CZ", ThreeLettersCode = "CZE", ThreeDigitsCode = "203", InternetPrefix = ".cz", IsActive = true },
                new CountryData() { Name = "Chile", OfficialName = "República de Chile", TwoLettersCode = "CL", ThreeLettersCode = "CHL", ThreeDigitsCode = "152", InternetPrefix = ".cl", IsActive = true },
                new CountryData() { Name = "China", OfficialName = "República Popular de China", TwoLettersCode = "CN", ThreeLettersCode = "CHN", ThreeDigitsCode = "156", InternetPrefix = ".cn", IsActive = true },
                new CountryData() { Name = "Chipre", OfficialName = "República de Chipre", TwoLettersCode = "CY", ThreeLettersCode = "CYP", ThreeDigitsCode = "196", InternetPrefix = ".cy", IsActive = true },
                new CountryData() { Name = "Colombia", OfficialName = "República de Colombia", TwoLettersCode = "CO", ThreeLettersCode = "COL", ThreeDigitsCode = "170", InternetPrefix = ".co", IsActive = true },
                new CountryData() { Name = "Comoras", OfficialName = "Unión de las Comoras", TwoLettersCode = "KM", ThreeLettersCode = "COM", ThreeDigitsCode = "174", InternetPrefix = ".km", IsActive = true },
                new CountryData() { Name = "Corea del Norte", OfficialName = "República Popular Democrática de Corea", TwoLettersCode = "KP", ThreeLettersCode = "PRK", ThreeDigitsCode = "408", InternetPrefix = ".kp", IsActive = true },
                new CountryData() { Name = "Corea del Sur", OfficialName = "República de Corea", TwoLettersCode = "KR", ThreeLettersCode = "KOR", ThreeDigitsCode = "410", InternetPrefix = ".kr", IsActive = true },
                new CountryData() { Name = "Costa de Marfil", OfficialName = "República de Côte d Ivoire", TwoLettersCode = "CI", ThreeLettersCode = "CIV", ThreeDigitsCode = "384", InternetPrefix = ".ci", IsActive = true },
                new CountryData() { Name = "Costa Rica", OfficialName = "República de Costa Rica", TwoLettersCode = "CR", ThreeLettersCode = "CRI", ThreeDigitsCode = "188", InternetPrefix = ".cr", IsActive = true },
                new CountryData() { Name = "Croacia", OfficialName = "República de Croacia", TwoLettersCode = "HR", ThreeLettersCode = "HRV", ThreeDigitsCode = "191", InternetPrefix = ".hr", IsActive = true },
                new CountryData() { Name = "Cuba", OfficialName = "República de Cuba", TwoLettersCode = "CU", ThreeLettersCode = "CUB", ThreeDigitsCode = "192", InternetPrefix = ".cu", IsActive = true },
                new CountryData() { Name = "Curazao", OfficialName = "País de Curaçao", TwoLettersCode = "CW", ThreeLettersCode = "CUW", ThreeDigitsCode = "531", InternetPrefix = ".cw", IsActive = true },
                new CountryData() { Name = "Dinamarca", OfficialName = "Reino de Dinamarca", TwoLettersCode = "DK", ThreeLettersCode = "DNK", ThreeDigitsCode = "208", InternetPrefix = ".dk", IsActive = true },
                new CountryData() { Name = "Dominica", OfficialName = "Mancomunidad de Dominica", TwoLettersCode = "DM", ThreeLettersCode = "DMA", ThreeDigitsCode = "212", InternetPrefix = ".dm", IsActive = true },
                new CountryData() { Name = "Ecuador", OfficialName = "República del Ecuador", TwoLettersCode = "EC", ThreeLettersCode = "ECU", ThreeDigitsCode = "218", InternetPrefix = ".ec", IsActive = true },
                new CountryData() { Name = "Egipto", OfficialName = "República Árabe de Egipto", TwoLettersCode = "EG", ThreeLettersCode = "EGY", ThreeDigitsCode = "818", InternetPrefix = ".eg", IsActive = true },
                new CountryData() { Name = "El Salvador", OfficialName = "República de El Salvador", TwoLettersCode = "SV", ThreeLettersCode = "SLV", ThreeDigitsCode = "222", InternetPrefix = ".sv", IsActive = true },
                new CountryData() { Name = "Emiratos Árabes Unidos", OfficialName = "Emiratos Árabes Unidos", TwoLettersCode = "AE", ThreeLettersCode = "ARE", ThreeDigitsCode = "784", InternetPrefix = ".ae", IsActive = true },
                new CountryData() { Name = "Eritrea", OfficialName = "Estado de Eritrea", TwoLettersCode = "ER", ThreeLettersCode = "ERI", ThreeDigitsCode = "232", InternetPrefix = ".er", IsActive = true },
                new CountryData() { Name = "Eslovaquia", OfficialName = "República Eslovaca", TwoLettersCode = "SK", ThreeLettersCode = "SVK", ThreeDigitsCode = "703", InternetPrefix = ".sk", IsActive = true },
                new CountryData() { Name = "Eslovenia", OfficialName = "República de Eslovenia", TwoLettersCode = "SI", ThreeLettersCode = "SVN", ThreeDigitsCode = "705", InternetPrefix = ".si", IsActive = true },
                new CountryData() { Name = "España", OfficialName = "Reino de España", TwoLettersCode = "ES", ThreeLettersCode = "ESP", ThreeDigitsCode = "724", InternetPrefix = ".es", IsActive = true },
                new CountryData() { Name = "Estados Unidos", OfficialName = "Estados Unidos de América", TwoLettersCode = "US", ThreeLettersCode = "USA", ThreeDigitsCode = "840", InternetPrefix = ".us", IsActive = true },
                new CountryData() { Name = "Estonia", OfficialName = "República de Estonia", TwoLettersCode = "EE", ThreeLettersCode = "EST", ThreeDigitsCode = "233", InternetPrefix = ".ee", IsActive = true },
                new CountryData() { Name = "Esuatini", OfficialName = "Reino de Esuatini", TwoLettersCode = "SZ", ThreeLettersCode = "SWZ", ThreeDigitsCode = "748", InternetPrefix = ".sz", IsActive = true },
                new CountryData() { Name = "Etiopía", OfficialName = "República Democrática Federal de Etiopía", TwoLettersCode = "ET", ThreeLettersCode = "ETH", ThreeDigitsCode = "231", InternetPrefix = ".et", IsActive = true },
                new CountryData() { Name = "Filipinas", OfficialName = "República de las Filipinas", TwoLettersCode = "PH", ThreeLettersCode = "PHL", ThreeDigitsCode = "608", InternetPrefix = ".ph", IsActive = true },
                new CountryData() { Name = "Finlandia", OfficialName = "República de Finlandia", TwoLettersCode = "FI", ThreeLettersCode = "FIN", ThreeDigitsCode = "246", InternetPrefix = ".fi", IsActive = true },
                new CountryData() { Name = "Fiyi", OfficialName = "República de Fiji", TwoLettersCode = "FJ", ThreeLettersCode = "FJI", ThreeDigitsCode = "242", InternetPrefix = ".fj", IsActive = true },
                new CountryData() { Name = "Francia", OfficialName = "República Francesa", TwoLettersCode = "FR", ThreeLettersCode = "FRA", ThreeDigitsCode = "250", InternetPrefix = ".fr", IsActive = true },
                new CountryData() { Name = "Gabón", OfficialName = "República Gabonesa", TwoLettersCode = "GA", ThreeLettersCode = "GAB", ThreeDigitsCode = "266", InternetPrefix = ".ga", IsActive = true },
                new CountryData() { Name = "Gambia", OfficialName = "República del Gambia", TwoLettersCode = "GM", ThreeLettersCode = "GMB", ThreeDigitsCode = "270", InternetPrefix = ".gm", IsActive = true },
                new CountryData() { Name = "Georgia", OfficialName = "Georgia", TwoLettersCode = "GE", ThreeLettersCode = "GEO", ThreeDigitsCode = "268", InternetPrefix = ".ge", IsActive = true },
                new CountryData() { Name = "Ghana", OfficialName = "República de Ghana", TwoLettersCode = "GH", ThreeLettersCode = "GHA", ThreeDigitsCode = "288", InternetPrefix = ".gh", IsActive = true },
                new CountryData() { Name = "Gibraltar", OfficialName = "Gibraltar", TwoLettersCode = "GI", ThreeLettersCode = "GIB", ThreeDigitsCode = "292", InternetPrefix = ".gi", IsActive = true },
                new CountryData() { Name = "Granada", OfficialName = "Granada", TwoLettersCode = "GD", ThreeLettersCode = "GRD", ThreeDigitsCode = "308", InternetPrefix = ".gd", IsActive = true },
                new CountryData() { Name = "Grecia", OfficialName = "República Helénica", TwoLettersCode = "GR", ThreeLettersCode = "GRC", ThreeDigitsCode = "300", InternetPrefix = ".gr", IsActive = true },
                new CountryData() { Name = "Groenlandia", OfficialName = "Kalaallit Nunaat", TwoLettersCode = "GL", ThreeLettersCode = "GRL", ThreeDigitsCode = "304", InternetPrefix = ".gl", IsActive = true },
                new CountryData() { Name = "Guadalupe", OfficialName = "Guadeloupe", TwoLettersCode = "GP", ThreeLettersCode = "GLP", ThreeDigitsCode = "312", InternetPrefix = ".gp", IsActive = true },
                new CountryData() { Name = "Guam", OfficialName = "Territorio de Guam", TwoLettersCode = "GU", ThreeLettersCode = "GUM", ThreeDigitsCode = "316", InternetPrefix = ".gu", IsActive = true },
                new CountryData() { Name = "Guatemala", OfficialName = "República de Guatemala", TwoLettersCode = "GT", ThreeLettersCode = "GTM", ThreeDigitsCode = "320", InternetPrefix = ".gt", IsActive = true },
                new CountryData() { Name = "Guayana Francesa", OfficialName = "Guayana Francesa", TwoLettersCode = "GF", ThreeLettersCode = "GUF", ThreeDigitsCode = "254", InternetPrefix = ".gf", IsActive = true },
                new CountryData() { Name = "Guernsey", OfficialName = "Bailía de Guernsey", TwoLettersCode = "GG", ThreeLettersCode = "GGY", ThreeDigitsCode = "831", InternetPrefix = ".gg", IsActive = true },
                new CountryData() { Name = "Guinea", OfficialName = "República de Guinea", TwoLettersCode = "GN", ThreeLettersCode = "GIN", ThreeDigitsCode = "324", InternetPrefix = ".gn", IsActive = true },
                new CountryData() { Name = "Guinea Ecuatorial", OfficialName = "República de Guinea Ecuatorial", TwoLettersCode = "GQ", ThreeLettersCode = "GNQ", ThreeDigitsCode = "226", InternetPrefix = ".gq", IsActive = true },
                new CountryData() { Name = "Guinea-Bisáu", OfficialName = "República de Guinea Bissau", TwoLettersCode = "GW", ThreeLettersCode = "GNB", ThreeDigitsCode = "624", InternetPrefix = ".gw", IsActive = true },
                new CountryData() { Name = "Guyana", OfficialName = "República Cooperativa de Guyana", TwoLettersCode = "GY", ThreeLettersCode = "GUY", ThreeDigitsCode = "328", InternetPrefix = ".gy", IsActive = true },
                new CountryData() { Name = "Haití", OfficialName = "República de Haití", TwoLettersCode = "HT", ThreeLettersCode = "HTI", ThreeDigitsCode = "332", InternetPrefix = ".ht", IsActive = true },
                new CountryData() { Name = "Honduras", OfficialName = "República de Honduras", TwoLettersCode = "HN", ThreeLettersCode = "HND", ThreeDigitsCode = "340", InternetPrefix = ".hn", IsActive = true },
                new CountryData() { Name = "Hong Kong", OfficialName = "Región Especial Administrativa de Hong Kong", TwoLettersCode = "HK", ThreeLettersCode = "HKG", ThreeDigitsCode = "344", InternetPrefix = ".hk", IsActive = true },
                new CountryData() { Name = "Hungría", OfficialName = "Hungría", TwoLettersCode = "HU", ThreeLettersCode = "HUN", ThreeDigitsCode = "348", InternetPrefix = ".hu", IsActive = true },
                new CountryData() { Name = "India", OfficialName = "República de India", TwoLettersCode = "IN", ThreeLettersCode = "IND", ThreeDigitsCode = "356", InternetPrefix = ".in", IsActive = true },
                new CountryData() { Name = "Indonesia", OfficialName = "República de Indonesia", TwoLettersCode = "ID", ThreeLettersCode = "IDN", ThreeDigitsCode = "360", InternetPrefix = ".id", IsActive = true },
                new CountryData() { Name = "Irak", OfficialName = "República de Iraq", TwoLettersCode = "IQ", ThreeLettersCode = "IRQ", ThreeDigitsCode = "368", InternetPrefix = ".iq", IsActive = true },
                new CountryData() { Name = "Irán", OfficialName = "República Islámica de Irán", TwoLettersCode = "IR", ThreeLettersCode = "IRN", ThreeDigitsCode = "364", InternetPrefix = ".ir", IsActive = true },
                new CountryData() { Name = "Irlanda", OfficialName = "Irlanda", TwoLettersCode = "IE", ThreeLettersCode = "IRL", ThreeDigitsCode = "372", InternetPrefix = ".ie", IsActive = true },
                new CountryData() { Name = "Isla Bouvet", OfficialName = "Isla Bouvet", TwoLettersCode = "BV", ThreeLettersCode = "BVT", ThreeDigitsCode = "074", InternetPrefix = ".bv", IsActive = true },
                new CountryData() { Name = "Isla de Man", OfficialName = "Isla de Man", TwoLettersCode = "IM", ThreeLettersCode = "IMN", ThreeDigitsCode = "833", InternetPrefix = ".im", IsActive = true },
                new CountryData() { Name = "Isla de Navidad", OfficialName = "Isla de Navidad", TwoLettersCode = "CX", ThreeLettersCode = "CXR", ThreeDigitsCode = "162", InternetPrefix = ".cx", IsActive = true },
                new CountryData() { Name = "Isla Norfolk", OfficialName = "Territorio de la Isla Norfolk", TwoLettersCode = "NF", ThreeLettersCode = "NFK", ThreeDigitsCode = "574", InternetPrefix = ".nf", IsActive = true },
                new CountryData() { Name = "Islandia", OfficialName = "Islandia", TwoLettersCode = "IS", ThreeLettersCode = "ISL", ThreeDigitsCode = "352", InternetPrefix = ".is", IsActive = true },
                new CountryData() { Name = "Islas Caimán", OfficialName = "Las Islas Caimán", TwoLettersCode = "KY", ThreeLettersCode = "CYM", ThreeDigitsCode = "136", InternetPrefix = ".ky", IsActive = true },
                new CountryData() { Name = "Islas Cocos", OfficialName = "Islas Cocos / Keeling", TwoLettersCode = "CC", ThreeLettersCode = "CCK", ThreeDigitsCode = "166", InternetPrefix = ".cc", IsActive = true },
                new CountryData() { Name = "Islas Cook", OfficialName = "Islas Cook", TwoLettersCode = "CK", ThreeLettersCode = "COK", ThreeDigitsCode = "184", InternetPrefix = ".ck", IsActive = true },
                new CountryData() { Name = "Islas Feroe", OfficialName = "Islas Feroe", TwoLettersCode = "FO", ThreeLettersCode = "FRO", ThreeDigitsCode = "234", InternetPrefix = ".fo", IsActive = true },
                new CountryData() { Name = "Islas Georgias del Sur y Sandwich del Sur", OfficialName = "Georgia del Sur y las Islas Sandwich del Sur", TwoLettersCode = "GS", ThreeLettersCode = "SGS", ThreeDigitsCode = "239", InternetPrefix = ".gs", IsActive = true },
                new CountryData() { Name = "Islas Heard y McDonald", OfficialName = "Territorio de Isla Heard e Islas McDonald", TwoLettersCode = "HM", ThreeLettersCode = "HMD", ThreeDigitsCode = "334", InternetPrefix = ".hm", IsActive = true },
                new CountryData() { Name = "Islas Malvinas", OfficialName = "Islas Falkland", TwoLettersCode = "FK", ThreeLettersCode = "FLK", ThreeDigitsCode = "238", InternetPrefix = ".fk", IsActive = true },
                new CountryData() { Name = "Islas Marianas del Norte", OfficialName = "Mancomunidad de las Islas Marianas del Norte", TwoLettersCode = "MP", ThreeLettersCode = "MNP", ThreeDigitsCode = "580", InternetPrefix = ".mp", IsActive = true },
                new CountryData() { Name = "Islas Marshall", OfficialName = "República de Islas Marshall", TwoLettersCode = "MH", ThreeLettersCode = "MHL", ThreeDigitsCode = "584", InternetPrefix = ".mh", IsActive = true },
                new CountryData() { Name = "Islas Pitcairn", OfficialName = "Pitcairn", TwoLettersCode = "PN", ThreeLettersCode = "PCN", ThreeDigitsCode = "612", InternetPrefix = ".pn", IsActive = true },
                new CountryData() { Name = "Islas Salomón", OfficialName = "Islas Salomón", TwoLettersCode = "SB", ThreeLettersCode = "SLB", ThreeDigitsCode = "90 ", InternetPrefix = ".sb", IsActive = true },
                new CountryData() { Name = "Islas Turcas y Caicos", OfficialName = "Islas Turcas y Caicos", TwoLettersCode = "TC", ThreeLettersCode = "TCA", ThreeDigitsCode = "796", InternetPrefix = ".tc", IsActive = true },
                new CountryData() { Name = "Islas Ultramarinas Menores de los Estados Unidos", OfficialName = "Islas Ultramarinas Menores de los Estados Unidos", TwoLettersCode = "UM", ThreeLettersCode = "UMI", ThreeDigitsCode = "581", InternetPrefix = ".um", IsActive = true },
                new CountryData() { Name = "Islas Vírgenes Británicas", OfficialName = "Islas Vírgenes Británicas", TwoLettersCode = "VG", ThreeLettersCode = "VGB", ThreeDigitsCode = "92 ", InternetPrefix = ".vg", IsActive = true },
                new CountryData() { Name = "Islas Vírgenes de los Estados Unidos", OfficialName = "Islas Vírgenes de los Estados Unidos", TwoLettersCode = "VI", ThreeLettersCode = "VIR", ThreeDigitsCode = "850", InternetPrefix = ".vi", IsActive = true },
                new CountryData() { Name = "Israel", OfficialName = "Estado de Israel", TwoLettersCode = "IL", ThreeLettersCode = "ISR", ThreeDigitsCode = "376", InternetPrefix = ".il", IsActive = true },
                new CountryData() { Name = "Italia", OfficialName = "República Italiana", TwoLettersCode = "IT", ThreeLettersCode = "ITA", ThreeDigitsCode = "380", InternetPrefix = ".it", IsActive = true },
                new CountryData() { Name = "Jamaica", OfficialName = "Jamaica", TwoLettersCode = "JM", ThreeLettersCode = "JAM", ThreeDigitsCode = "388", InternetPrefix = ".jm", IsActive = true },
                new CountryData() { Name = "Japón", OfficialName = "Japón", TwoLettersCode = "JP", ThreeLettersCode = "JPN", ThreeDigitsCode = "392", InternetPrefix = ".jp", IsActive = true },
                new CountryData() { Name = "Jersey", OfficialName = "Bailía de Jersey", TwoLettersCode = "JE", ThreeLettersCode = "JEY", ThreeDigitsCode = "832", InternetPrefix = ".je", IsActive = true },
                new CountryData() { Name = "Jordania", OfficialName = "Reino Hachemita de Jordania", TwoLettersCode = "JO", ThreeLettersCode = "JOR", ThreeDigitsCode = "400", InternetPrefix = ".jo", IsActive = true },
                new CountryData() { Name = "Kazajistán", OfficialName = "República de Kazajistán", TwoLettersCode = "KZ", ThreeLettersCode = "KAZ", ThreeDigitsCode = "398", InternetPrefix = ".kz", IsActive = true },
                new CountryData() { Name = "Kenia", OfficialName = "República de Kenia", TwoLettersCode = "KE", ThreeLettersCode = "KEN", ThreeDigitsCode = "404", InternetPrefix = ".ke", IsActive = true },
                new CountryData() { Name = "Kirguistán", OfficialName = "República Kirguisa", TwoLettersCode = "KG", ThreeLettersCode = "KGZ", ThreeDigitsCode = "417", InternetPrefix = ".kg", IsActive = true },
                new CountryData() { Name = "Kiribati", OfficialName = "República de Kiribati", TwoLettersCode = "KI", ThreeLettersCode = "KIR", ThreeDigitsCode = "296", InternetPrefix = ".ki", IsActive = true },
                new CountryData() { Name = "Kuwait", OfficialName = "Estado de Kuwait", TwoLettersCode = "KW", ThreeLettersCode = "KWT", ThreeDigitsCode = "414", InternetPrefix = ".kw", IsActive = true },
                new CountryData() { Name = "Laos", OfficialName = "República Democrática Popular Lao", TwoLettersCode = "LA", ThreeLettersCode = "LAO", ThreeDigitsCode = "418", InternetPrefix = ".la", IsActive = true },
                new CountryData() { Name = "Lesoto", OfficialName = "Reino de Lesoto", TwoLettersCode = "LS", ThreeLettersCode = "LSO", ThreeDigitsCode = "426", InternetPrefix = ".ls", IsActive = true },
                new CountryData() { Name = "Letonia", OfficialName = "República de Letonia", TwoLettersCode = "LV", ThreeLettersCode = "LVA", ThreeDigitsCode = "428", InternetPrefix = ".lv", IsActive = true },
                new CountryData() { Name = "Líbano", OfficialName = "República Libanesa", TwoLettersCode = "LB", ThreeLettersCode = "LBN", ThreeDigitsCode = "422", InternetPrefix = ".lb", IsActive = true },
                new CountryData() { Name = "Liberia", OfficialName = "República de Liberia", TwoLettersCode = "LR", ThreeLettersCode = "LBR", ThreeDigitsCode = "430", InternetPrefix = ".lr", IsActive = true },
                new CountryData() { Name = "Libia", OfficialName = "Estado de Libia", TwoLettersCode = "LY", ThreeLettersCode = "LBY", ThreeDigitsCode = "434", InternetPrefix = ".ly", IsActive = true },
                new CountryData() { Name = "Liechtenstein", OfficialName = "Principado de Liechtenstein", TwoLettersCode = "LI", ThreeLettersCode = "LIE", ThreeDigitsCode = "438", InternetPrefix = ".li", IsActive = true },
                new CountryData() { Name = "Lituania", OfficialName = "República de Lituania", TwoLettersCode = "LT", ThreeLettersCode = "LTU", ThreeDigitsCode = "440", InternetPrefix = ".lt", IsActive = true },
                new CountryData() { Name = "Luxemburgo", OfficialName = "Gran Ducado de Luxemburgo", TwoLettersCode = "LU", ThreeLettersCode = "LUX", ThreeDigitsCode = "442", InternetPrefix = ".lu", IsActive = true },
                new CountryData() { Name = "Macao", OfficialName = "Región Especial Administrativa de Macao", TwoLettersCode = "MO", ThreeLettersCode = "MAC", ThreeDigitsCode = "446", InternetPrefix = ".mo", IsActive = true },
                new CountryData() { Name = "Macedonia del Norte", OfficialName = "República de Macedonia del Norte", TwoLettersCode = "MK", ThreeLettersCode = "MKD", ThreeDigitsCode = "807", InternetPrefix = ".mk", IsActive = true },
                new CountryData() { Name = "Madagascar", OfficialName = "República de Madagascar", TwoLettersCode = "MG", ThreeLettersCode = "MDG", ThreeDigitsCode = "450", InternetPrefix = ".mg", IsActive = true },
                new CountryData() { Name = "Malasia", OfficialName = "Malasia", TwoLettersCode = "MY", ThreeLettersCode = "MYS", ThreeDigitsCode = "458", InternetPrefix = ".my", IsActive = true },
                new CountryData() { Name = "Malaui", OfficialName = "República de Malawi", TwoLettersCode = "MW", ThreeLettersCode = "MWI", ThreeDigitsCode = "454", InternetPrefix = ".mw", IsActive = true },
                new CountryData() { Name = "Maldivas", OfficialName = "República de Maldivas", TwoLettersCode = "MV", ThreeLettersCode = "MDV", ThreeDigitsCode = "462", InternetPrefix = ".mv", IsActive = true },
                new CountryData() { Name = "Malí", OfficialName = "República de Malí", TwoLettersCode = "ML", ThreeLettersCode = "MLI", ThreeDigitsCode = "466", InternetPrefix = ".ml", IsActive = true },
                new CountryData() { Name = "Malta", OfficialName = "República de Malta", TwoLettersCode = "MT", ThreeLettersCode = "MLT", ThreeDigitsCode = "470", InternetPrefix = ".mt", IsActive = true },
                new CountryData() { Name = "Marruecos", OfficialName = "Reino de Marruecos", TwoLettersCode = "MA", ThreeLettersCode = "MAR", ThreeDigitsCode = "504", InternetPrefix = ".ma", IsActive = true },
                new CountryData() { Name = "Martinica", OfficialName = "Martinique", TwoLettersCode = "MQ", ThreeLettersCode = "MTQ", ThreeDigitsCode = "474", InternetPrefix = ".mq", IsActive = true },
                new CountryData() { Name = "Mauricio", OfficialName = "República de Mauricio", TwoLettersCode = "MU", ThreeLettersCode = "MUS", ThreeDigitsCode = "480", InternetPrefix = ".mu", IsActive = true },
                new CountryData() { Name = "Mauritania", OfficialName = "República de Mauritania", TwoLettersCode = "MR", ThreeLettersCode = "MRT", ThreeDigitsCode = "478", InternetPrefix = ".mr", IsActive = true },
                new CountryData() { Name = "Mayotte", OfficialName = "Departamento de Mayotte", TwoLettersCode = "YT", ThreeLettersCode = "MYT", ThreeDigitsCode = "175", InternetPrefix = ".yt", IsActive = true },
                new CountryData() { Name = "México", OfficialName = "Estados Unidos Mexicanos", TwoLettersCode = "MX", ThreeLettersCode = "MEX", ThreeDigitsCode = "484", InternetPrefix = ".mx", IsActive = true },
                new CountryData() { Name = "Micronesia", OfficialName = "Estados Federados de Micronesia", TwoLettersCode = "FM", ThreeLettersCode = "FSM", ThreeDigitsCode = "583", InternetPrefix = ".fm", IsActive = true },
                new CountryData() { Name = "Moldavia", OfficialName = "República de Moldova", TwoLettersCode = "MD", ThreeLettersCode = "MDA", ThreeDigitsCode = "498", InternetPrefix = ".md", IsActive = true },
                new CountryData() { Name = "Mónaco", OfficialName = "Principado de Mónaco", TwoLettersCode = "MC", ThreeLettersCode = "MCO", ThreeDigitsCode = "492", InternetPrefix = ".mc", IsActive = true },
                new CountryData() { Name = "Mongolia", OfficialName = "Mongolia", TwoLettersCode = "MN", ThreeLettersCode = "MNG", ThreeDigitsCode = "496", InternetPrefix = ".mn", IsActive = true },
                new CountryData() { Name = "Montenegro", OfficialName = "Montenegro", TwoLettersCode = "ME", ThreeLettersCode = "MNE", ThreeDigitsCode = "499", InternetPrefix = ".me", IsActive = true },
                new CountryData() { Name = "Montserrat", OfficialName = "Montserrat", TwoLettersCode = "MS", ThreeLettersCode = "MSR", ThreeDigitsCode = "500", InternetPrefix = ".ms", IsActive = true },
                new CountryData() { Name = "Mozambique", OfficialName = "República de Mozambique", TwoLettersCode = "MZ", ThreeLettersCode = "MOZ", ThreeDigitsCode = "508", InternetPrefix = ".mz", IsActive = true },
                new CountryData() { Name = "Myanmar", OfficialName = "República de la Unión de Myanmar", TwoLettersCode = "MM", ThreeLettersCode = "MMR", ThreeDigitsCode = "104", InternetPrefix = ".mm", IsActive = true },
                new CountryData() { Name = "Namibia", OfficialName = "República de Namibia", TwoLettersCode = "NA", ThreeLettersCode = "NAM", ThreeDigitsCode = "516", InternetPrefix = ".na", IsActive = true },
                new CountryData() { Name = "Nauru", OfficialName = "República de Nauru", TwoLettersCode = "NR", ThreeLettersCode = "NRU", ThreeDigitsCode = "520", InternetPrefix = ".nr", IsActive = true },
                new CountryData() { Name = "Nepal", OfficialName = "República Democrática Federal de Nepal", TwoLettersCode = "NP", ThreeLettersCode = "NPL", ThreeDigitsCode = "524", InternetPrefix = ".np", IsActive = true },
                new CountryData() { Name = "Nicaragua", OfficialName = "República de Nicaragua", TwoLettersCode = "NI", ThreeLettersCode = "NIC", ThreeDigitsCode = "558", InternetPrefix = ".ni", IsActive = true },
                new CountryData() { Name = "Níger", OfficialName = "República del Níger", TwoLettersCode = "NE", ThreeLettersCode = "NER", ThreeDigitsCode = "562", InternetPrefix = ".ne", IsActive = true },
                new CountryData() { Name = "Nigeria", OfficialName = "República Federal de Nigeria", TwoLettersCode = "NG", ThreeLettersCode = "NGA", ThreeDigitsCode = "566", InternetPrefix = ".ng", IsActive = true },
                new CountryData() { Name = "Niue", OfficialName = "Niue", TwoLettersCode = "NU", ThreeLettersCode = "NIU", ThreeDigitsCode = "570", InternetPrefix = ".nu", IsActive = true },
                new CountryData() { Name = "Noruega", OfficialName = "Reino de Noruega", TwoLettersCode = "NO", ThreeLettersCode = "NOR", ThreeDigitsCode = "578", InternetPrefix = ".no", IsActive = true },
                new CountryData() { Name = "Nueva Caledonia", OfficialName = "Nueva Caledonia", TwoLettersCode = "NC", ThreeLettersCode = "NCL", ThreeDigitsCode = "540", InternetPrefix = ".nc", IsActive = true },
                new CountryData() { Name = "Nueva Zelandia", OfficialName = "Nueva Zelandia", TwoLettersCode = "NZ", ThreeLettersCode = "NZL", ThreeDigitsCode = "554", InternetPrefix = ".nz", IsActive = true },
                new CountryData() { Name = "Omán", OfficialName = "Sultanato de Omán", TwoLettersCode = "OM", ThreeLettersCode = "OMN", ThreeDigitsCode = "512", InternetPrefix = ".om", IsActive = true },
                new CountryData() { Name = "Países Bajos", OfficialName = "Reino de los Países Bajos", TwoLettersCode = "NL", ThreeLettersCode = "NLD", ThreeDigitsCode = "528", InternetPrefix = ".nl", IsActive = true },
                new CountryData() { Name = "Pakistán", OfficialName = "República Islámica de Pakistán", TwoLettersCode = "PK", ThreeLettersCode = "PAK", ThreeDigitsCode = "586", InternetPrefix = ".pk", IsActive = true },
                new CountryData() { Name = "Palau", OfficialName = "República de Palau", TwoLettersCode = "PW", ThreeLettersCode = "PLW", ThreeDigitsCode = "585", InternetPrefix = ".pw", IsActive = true },
                new CountryData() { Name = "Palestina", OfficialName = "Estado de Palestina", TwoLettersCode = "PS", ThreeLettersCode = "PSE", ThreeDigitsCode = "275", InternetPrefix = ".ps", IsActive = true },
                new CountryData() { Name = "Panamá", OfficialName = "República de Panamá", TwoLettersCode = "PA", ThreeLettersCode = "PAN", ThreeDigitsCode = "591", InternetPrefix = ".pa", IsActive = true },
                new CountryData() { Name = "Papúa Nueva Guinea", OfficialName = "Estado Independiente de Papúa Nueva Guinea", TwoLettersCode = "PG", ThreeLettersCode = "PNG", ThreeDigitsCode = "598", InternetPrefix = ".pg", IsActive = true },
                new CountryData() { Name = "Paraguay", OfficialName = "República de Paraguay", TwoLettersCode = "PY", ThreeLettersCode = "PRY", ThreeDigitsCode = "600", InternetPrefix = ".py", IsActive = true },
                new CountryData() { Name = "Perú", OfficialName = "República de Perú", TwoLettersCode = "PE", ThreeLettersCode = "PER", ThreeDigitsCode = "604", InternetPrefix = ".pe", IsActive = true },
                new CountryData() { Name = "Polinesia Francesa", OfficialName = "Polinesia Francesa", TwoLettersCode = "PF", ThreeLettersCode = "PYF", ThreeDigitsCode = "258", InternetPrefix = ".pf", IsActive = true },
                new CountryData() { Name = "Polonia", OfficialName = "República de Polonia", TwoLettersCode = "PL", ThreeLettersCode = "POL", ThreeDigitsCode = "616", InternetPrefix = ".pl", IsActive = true },
                new CountryData() { Name = "Portugal", OfficialName = "República Portuguesa", TwoLettersCode = "PT", ThreeLettersCode = "PRT", ThreeDigitsCode = "620", InternetPrefix = ".pt", IsActive = true },
                new CountryData() { Name = "Puerto Rico", OfficialName = "Estado Libre Asociado de Puerto Rico", TwoLettersCode = "PR", ThreeLettersCode = "PRI", ThreeDigitsCode = "633", InternetPrefix = ".pr", IsActive = true },
                new CountryData() { Name = "Reino Unido", OfficialName = "Reino Unido de Gran Bretaña e Irlanda del Norte", TwoLettersCode = "GB", ThreeLettersCode = "GBR", ThreeDigitsCode = "826", InternetPrefix = ".uk", IsActive = true },
                new CountryData() { Name = "República Árabe Saharaui Democrática", OfficialName = "República Árabe Saharaui Democrática", TwoLettersCode = "EH", ThreeLettersCode = "ESH", ThreeDigitsCode = "732", InternetPrefix = ".eh", IsActive = true },
                new CountryData() { Name = "República Centroafricana", OfficialName = "La República Centroafricana", TwoLettersCode = "CF", ThreeLettersCode = "CAF", ThreeDigitsCode = "140", InternetPrefix = ".cf", IsActive = true },
                new CountryData() { Name = "República del Congo", OfficialName = "República del Congo", TwoLettersCode = "CG", ThreeLettersCode = "COG", ThreeDigitsCode = "178", InternetPrefix = ".cg", IsActive = true },
                new CountryData() { Name = "República Democrática del Congo", OfficialName = "República Democrática del Congo", TwoLettersCode = "CD", ThreeLettersCode = "COD", ThreeDigitsCode = "180", InternetPrefix = ".cd", IsActive = true },
                new CountryData() { Name = "República Dominicana", OfficialName = "República Dominicana", TwoLettersCode = "DO", ThreeLettersCode = "DOM", ThreeDigitsCode = "214", InternetPrefix = ".do", IsActive = true },
                new CountryData() { Name = "Reunión", OfficialName = "Reunión", TwoLettersCode = "RE", ThreeLettersCode = "REU", ThreeDigitsCode = "638", InternetPrefix = ".re", IsActive = true },
                new CountryData() { Name = "Ruanda", OfficialName = "República de Ruanda", TwoLettersCode = "RW", ThreeLettersCode = "RWA", ThreeDigitsCode = "646", InternetPrefix = ".rw", IsActive = true },
                new CountryData() { Name = "Rumania", OfficialName = "Rumania", TwoLettersCode = "RO", ThreeLettersCode = "ROU", ThreeDigitsCode = "642", InternetPrefix = ".ro", IsActive = true },
                new CountryData() { Name = "Rusia", OfficialName = "Federación de Rusia", TwoLettersCode = "RU", ThreeLettersCode = "RUS", ThreeDigitsCode = "643", InternetPrefix = ".ru", IsActive = true },
                new CountryData() { Name = "Saint Martin", OfficialName = "Colectividad de Saint-Martin", TwoLettersCode = "MF", ThreeLettersCode = "MAF", ThreeDigitsCode = "663", InternetPrefix = ".mf", IsActive = true },
                new CountryData() { Name = "Samoa", OfficialName = "Estado de Samoa", TwoLettersCode = "WS", ThreeLettersCode = "WSM", ThreeDigitsCode = "882", InternetPrefix = ".ws", IsActive = true },
                new CountryData() { Name = "Samoa Americana", OfficialName = "Territorio de Samoa Americana", TwoLettersCode = "AS", ThreeLettersCode = "ASM", ThreeDigitsCode = "016", InternetPrefix = ".as", IsActive = true },
                new CountryData() { Name = "San Bartolomé", OfficialName = "Colectividad de Saint Barthélemy", TwoLettersCode = "BL", ThreeLettersCode = "BLM", ThreeDigitsCode = "652", InternetPrefix = ".bl", IsActive = true },
                new CountryData() { Name = "San Cristóbal y Nieves", OfficialName = "San Cristóbal y Nieves", TwoLettersCode = "KN", ThreeLettersCode = "KNA", ThreeDigitsCode = "659", InternetPrefix = ".kn", IsActive = true },
                new CountryData() { Name = "San Marino", OfficialName = "República de San Marino", TwoLettersCode = "SM", ThreeLettersCode = "SMR", ThreeDigitsCode = "674", InternetPrefix = ".sm", IsActive = true },
                new CountryData() { Name = "San Pedro y Miquelón", OfficialName = "Colectividad de Ultramar de San Pedro y Miquelón", TwoLettersCode = "PM", ThreeLettersCode = "SPM", ThreeDigitsCode = "666", InternetPrefix = ".pm", IsActive = true },
                new CountryData() { Name = "San Vicente y las Granadinas", OfficialName = "San Vicente y las Granadinas", TwoLettersCode = "VC", ThreeLettersCode = "VCT", ThreeDigitsCode = "670", InternetPrefix = ".vc", IsActive = true },
                new CountryData() { Name = "Santa Helena, Ascensión y Tristán de Acuña", OfficialName = "Santa Helena, Ascensión y Tristán de Acuña", TwoLettersCode = "SH", ThreeLettersCode = "SHN", ThreeDigitsCode = "654", InternetPrefix = ".sh", IsActive = true },
                new CountryData() { Name = "Santa Lucía", OfficialName = "Santa Lucía", TwoLettersCode = "LC", ThreeLettersCode = "LCA", ThreeDigitsCode = "662", InternetPrefix = ".lc", IsActive = true },
                new CountryData() { Name = "Santa Sede", OfficialName = "Santa Sede", TwoLettersCode = "VA", ThreeLettersCode = "VAT", ThreeDigitsCode = "336", InternetPrefix = ".va", IsActive = true },
                new CountryData() { Name = "Santo Tomé y Príncipe", OfficialName = "República Democrática de Santo Tomé y Príncipe", TwoLettersCode = "ST", ThreeLettersCode = "STP", ThreeDigitsCode = "678", InternetPrefix = ".st", IsActive = true },
                new CountryData() { Name = "Senegal", OfficialName = "República de Senegal", TwoLettersCode = "SN", ThreeLettersCode = "SEN", ThreeDigitsCode = "686", InternetPrefix = ".sn", IsActive = true },
                new CountryData() { Name = "Serbia", OfficialName = "República de Serbia", TwoLettersCode = "RS", ThreeLettersCode = "SRB", ThreeDigitsCode = "688", InternetPrefix = ".rs", IsActive = true },
                new CountryData() { Name = "Seychelles", OfficialName = "República de Seychelles", TwoLettersCode = "SC", ThreeLettersCode = "SYC", ThreeDigitsCode = "690", InternetPrefix = ".sc", IsActive = true },
                new CountryData() { Name = "Sierra Leona", OfficialName = "República de Sierra Leona", TwoLettersCode = "SL", ThreeLettersCode = "SLE", ThreeDigitsCode = "694", InternetPrefix = ".sl", IsActive = true },
                new CountryData() { Name = "Singapur", OfficialName = "República de Singapur", TwoLettersCode = "SG", ThreeLettersCode = "SGP", ThreeDigitsCode = "702", InternetPrefix = ".sg", IsActive = true },
                new CountryData() { Name = "Sint Maarten", OfficialName = "Sint Maarten", TwoLettersCode = "SX", ThreeLettersCode = "SXM", ThreeDigitsCode = "534", InternetPrefix = ".sx", IsActive = true },
                new CountryData() { Name = "Siria", OfficialName = "República Árabe Siria", TwoLettersCode = "SY", ThreeLettersCode = "SYR", ThreeDigitsCode = "760", InternetPrefix = ".sy", IsActive = true },
                new CountryData() { Name = "Somalia", OfficialName = "República Federal de Somalia", TwoLettersCode = "SO", ThreeLettersCode = "SOM", ThreeDigitsCode = "706", InternetPrefix = ".so", IsActive = true },
                new CountryData() { Name = "Sri Lanka", OfficialName = "República Socialista Democrática de Sri Lanka", TwoLettersCode = "LK", ThreeLettersCode = "LKA", ThreeDigitsCode = "144", InternetPrefix = ".lk", IsActive = true },
                new CountryData() { Name = "Sudáfrica", OfficialName = "República de Sudáfrica", TwoLettersCode = "ZA", ThreeLettersCode = "ZAF", ThreeDigitsCode = "710", InternetPrefix = ".za", IsActive = true },
                new CountryData() { Name = "Sudán", OfficialName = "República del Sudán", TwoLettersCode = "SD", ThreeLettersCode = "SDN", ThreeDigitsCode = "729", InternetPrefix = ".sd", IsActive = true },
                new CountryData() { Name = "Sudán del Sur", OfficialName = "República de Sudán del Sur", TwoLettersCode = "SS", ThreeLettersCode = "SSD", ThreeDigitsCode = "728", InternetPrefix = ".ss", IsActive = true },
                new CountryData() { Name = "Suecia", OfficialName = "Reino de Suecia", TwoLettersCode = "SE", ThreeLettersCode = "SWE", ThreeDigitsCode = "752", InternetPrefix = ".se", IsActive = true },
                new CountryData() { Name = "Suiza", OfficialName = "Confederación Suiza", TwoLettersCode = "CH", ThreeLettersCode = "CHE", ThreeDigitsCode = "756", InternetPrefix = ".ch", IsActive = true },
                new CountryData() { Name = "Suriname", OfficialName = "República de Suriname", TwoLettersCode = "SR", ThreeLettersCode = "SUR", ThreeDigitsCode = "740", InternetPrefix = ".sr", IsActive = true },
                new CountryData() { Name = "Svalbard y Jan Mayen", OfficialName = "Svalbard y Jan Mayen", TwoLettersCode = "SJ", ThreeLettersCode = "SJM", ThreeDigitsCode = "744", InternetPrefix = ".sj", IsActive = true },
                new CountryData() { Name = "Tailandia", OfficialName = "Reino de Tailandia", TwoLettersCode = "TH", ThreeLettersCode = "THA", ThreeDigitsCode = "764", InternetPrefix = ".th", IsActive = true },
                new CountryData() { Name = "Taiwán", OfficialName = "República de China", TwoLettersCode = "TW", ThreeLettersCode = "TWN", ThreeDigitsCode = "158", InternetPrefix = ".tw", IsActive = true },
                new CountryData() { Name = "Tanzania", OfficialName = "República Unida de Tanzania", TwoLettersCode = "TZ", ThreeLettersCode = "TZA", ThreeDigitsCode = "834", InternetPrefix = ".tz", IsActive = true },
                new CountryData() { Name = "Tayikistán", OfficialName = "República de Tayikistán", TwoLettersCode = "TJ", ThreeLettersCode = "TJK", ThreeDigitsCode = "762", InternetPrefix = ".tj", IsActive = true },
                new CountryData() { Name = "Territorio Británico del Océano Índico", OfficialName = "El Territorio Británico del Océano Índico", TwoLettersCode = "IO", ThreeLettersCode = "IOT", ThreeDigitsCode = "086", InternetPrefix = ".io", IsActive = true },
                new CountryData() { Name = "Tierras Australes y Antárticas Francesas", OfficialName = "Tierras Australes y Antárticas Francesas", TwoLettersCode = "TF", ThreeLettersCode = "ATF", ThreeDigitsCode = "260", InternetPrefix = ".tf", IsActive = true },
                new CountryData() { Name = "Timor Oriental", OfficialName = "República Democrática de Timor-Leste", TwoLettersCode = "TL", ThreeLettersCode = "TLS", ThreeDigitsCode = "626", InternetPrefix = ".tl", IsActive = true },
                new CountryData() { Name = "Togo", OfficialName = "República Togolesa", TwoLettersCode = "TG", ThreeLettersCode = "TGO", ThreeDigitsCode = "768", InternetPrefix = ".tg", IsActive = true },
                new CountryData() { Name = "Tokelau", OfficialName = "Tokelau", TwoLettersCode = "TK", ThreeLettersCode = "TKL", ThreeDigitsCode = "772", InternetPrefix = ".tk", IsActive = true },
                new CountryData() { Name = "Tonga", OfficialName = "Reino de Tonga", TwoLettersCode = "TO", ThreeLettersCode = "TON", ThreeDigitsCode = "776", InternetPrefix = ".to", IsActive = true },
                new CountryData() { Name = "Trinidad y Tobago", OfficialName = "República de Trinidad y Tobago", TwoLettersCode = "TT", ThreeLettersCode = "TTO", ThreeDigitsCode = "780", InternetPrefix = ".tt", IsActive = true },
                new CountryData() { Name = "Túnez", OfficialName = "República de Túnez", TwoLettersCode = "TN", ThreeLettersCode = "TUN", ThreeDigitsCode = "788", InternetPrefix = ".tn", IsActive = true },
                new CountryData() { Name = "Turkmenistán", OfficialName = "Turkmenistán", TwoLettersCode = "TM", ThreeLettersCode = "TKM", ThreeDigitsCode = "795", InternetPrefix = ".tm", IsActive = true },
                new CountryData() { Name = "Turquía", OfficialName = "República de Turquía", TwoLettersCode = "TR", ThreeLettersCode = "TUR", ThreeDigitsCode = "792", InternetPrefix = ".tr", IsActive = true },
                new CountryData() { Name = "Tuvalu", OfficialName = "Tuvalu", TwoLettersCode = "TV", ThreeLettersCode = "TUV", ThreeDigitsCode = "798", InternetPrefix = ".tv", IsActive = true },
                new CountryData() { Name = "Ucrania", OfficialName = "Ucrania", TwoLettersCode = "UA", ThreeLettersCode = "UKR", ThreeDigitsCode = "804", InternetPrefix = ".ua", IsActive = true },
                new CountryData() { Name = "Uganda", OfficialName = "República de Uganda", TwoLettersCode = "UG", ThreeLettersCode = "UGA", ThreeDigitsCode = "800", InternetPrefix = ".ug", IsActive = true },
                new CountryData() { Name = "Uruguay", OfficialName = "República Oriental del Uruguay", TwoLettersCode = "UY", ThreeLettersCode = "URY", ThreeDigitsCode = "858", InternetPrefix = ".uy", IsActive = true },
                new CountryData() { Name = "Uzbekistán", OfficialName = "República de Uzbekistán", TwoLettersCode = "UZ", ThreeLettersCode = "UZB", ThreeDigitsCode = "860", InternetPrefix = ".uz", IsActive = true },
                new CountryData() { Name = "Vanuatu", OfficialName = "República de Vanuatu", TwoLettersCode = "VU", ThreeLettersCode = "VUT", ThreeDigitsCode = "548", InternetPrefix = ".vu", IsActive = true },
                new CountryData() { Name = "Venezuela", OfficialName = "República Bolivariana de Venezuela", TwoLettersCode = "VE", ThreeLettersCode = "VEN", ThreeDigitsCode = "862", InternetPrefix = ".ve", IsActive = true },
                new CountryData() { Name = "Vietnam", OfficialName = "República Socialista de Viet Nam", TwoLettersCode = "VN", ThreeLettersCode = "VNM", ThreeDigitsCode = "704", InternetPrefix = ".vn", IsActive = true },
                new CountryData() { Name = "Wallis y Futuna", OfficialName = "Colectividad de Wallis y Futuna", TwoLettersCode = "WF", ThreeLettersCode = "WLF", ThreeDigitsCode = "876", InternetPrefix = ".wf", IsActive = true },
                new CountryData() { Name = "Yemen", OfficialName = "República de Yemen", TwoLettersCode = "YE", ThreeLettersCode = "YEM", ThreeDigitsCode = "887", InternetPrefix = ".ye", IsActive = true },
                new CountryData() { Name = "Yibuti", OfficialName = "República de Djibouti", TwoLettersCode = "DJ", ThreeLettersCode = "DJI", ThreeDigitsCode = "262", InternetPrefix = ".dj", IsActive = true },
                new CountryData() { Name = "Zambia", OfficialName = "República de Zambia", TwoLettersCode = "ZM", ThreeLettersCode = "ZMB", ThreeDigitsCode = "894", InternetPrefix = ".zm", IsActive = true },
                new CountryData() { Name = "Zimbabue", OfficialName = "República de Zimbabwe", TwoLettersCode = "ZW", ThreeLettersCode = "ZWE", ThreeDigitsCode = "716", InternetPrefix = ".zw", IsActive = true }
            );

            context.SaveChanges();
        }

        #endregion
    }
}