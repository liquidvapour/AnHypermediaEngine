using System;
using System.Collections.Generic;
using System.Linq;
using Domain;

namespace InMemoryDb
{
    public static class CorridorFactory
    {
        #region Currencies
        private static readonly IList<Currency> Currencies = new[] {
            new Currency("AUD", "Australian dollar"), 
            new Currency("EUR", "Euro"), 
            new Currency("CAD", "Canadian dollar"), 
            new Currency("USD", "United States dollar"), 
            new Currency("BHD", "Bahraini dinar"), 
            new Currency("BRL", "Brazilian real"), 
            new Currency("BGN", "Bulgarian lev"), 
            new Currency("CZK", "Czech koruna"), 
            new Currency("DKK", "Danish krone"), 
            new Currency("GIP", "Gibraltar pound"), 
            new Currency("HKD", "Hong Kong dollar"), 
            new Currency("HUF", "Hungarian forint"), 
            new Currency("ISK", "Icelandic króna"), 
            new Currency("JPY", "Japanese yen"), 
            new Currency("JOD", "Jordanian dinar"), 
            new Currency("KWD", "Kuwaiti dinar"), 
            new Currency("MYR", "Malaysian ringgit"), 
            new Currency("NZD", "New Zealand dollar"), 
            new Currency("NOK", "Norwegian krone"), 
            new Currency("OMR", "Omani rial"), 
            new Currency("PHP", "Philippine peso"), 
            new Currency("PLN", "Polish złoty"), 
            new Currency("QAR", "Qatari riyal"), 
            new Currency("RON", "Romanian leu"), 
            new Currency("SAR", "Saudi riyal"), 
            new Currency("SGD", "Singapore dollar"), 
            new Currency("ZAR", "South African rand"), 
            new Currency("KPW", "North Korean won"), 
            new Currency("SEK", "Swedish krona/kronor"), 
            new Currency("CHF", "Swiss franc"), 
            new Currency("TWD", "New Taiwan dollar"), 
            new Currency("AED", "United Arab Emirates dirham"), 
            new Currency("GBP", "Pound sterling"), 
            new Currency("ALL", "Albanian lek"), 
            new Currency("XCD", "East Caribbean dollar"), 
            new Currency("ARS", "Argentine peso"), 
            new Currency("AMD", "Armenian dram"), 
            new Currency("BDT", "Bangladeshi taka"), 
            new Currency("BBD", "Barbados dollar"), 
            new Currency("XOF", "CFA franc BCEAO"), 
            new Currency("BOB", "Boliviano"), 
            new Currency("BAM", "Bosnia and Herzegovina convertible mark"), 
            new Currency("BIF", "Burundian franc"), 
            new Currency("KHR", "Cambodian riel"), 
            new Currency("XAF", "CFA franc BEAC"), 
            new Currency("CVE", "Cape Verde escudo"), 
            new Currency("KYD", "Cayman Islands dollar"), 
            new Currency("CLP", "Chilean peso"), 
            new Currency("CNY", "Chinese yuan"), 
            new Currency("COP", "Colombian peso"), 
            new Currency("KMF", "Comoro franc"), 
            new Currency("CDF", "Congolese franc"), 
            new Currency("CRC", "Costa Rican colon"), 
            new Currency("HRK", "Croatian kuna"), 
            new Currency("CUP", "Cuban peso"), 
            new Currency("DOP", "Dominican peso"), 
            new Currency("EGP", "Egyptian pound"), 
            new Currency("ETB", "Ethiopian birr"), 
            new Currency("FJD", "Fiji dollar"), 
            new Currency("GMD", "Gambian dalasi"), 
            new Currency("GHS", "Ghanaian cedi"), 
            new Currency("GTQ", "Guatemalan quetzal"), 
            new Currency("GNF", "Guinean franc"), 
            new Currency("HTG", "Haitian gourde"), 
            new Currency("HNL", "Honduran lempira"), 
            new Currency("INR", "Indian rupee"), 
            new Currency("IDR", "Indonesian rupiah"), 
            new Currency("ILS", "Israeli new shekel"), 
            new Currency("CFA", "Central African CFA Franc"), 
            new Currency("JMD", "Jamaican dollar"), 
            new Currency("KES", "Kenyan shilling"), 
            new Currency("LAK", "Lao kip"), 
            new Currency("LSL", "Lesotho loti"), 
            new Currency("LRD", "Liberian dollar"), 
            new Currency("MGA", "Malagasy ariary"), 
            new Currency("MRO", "Mauritanian ouguiya"), 
            new Currency("MXN", "Mexican peso"), 
            new Currency("MAD", "Moroccan dirham"), 
            new Currency("MZN", "Mozambican metical"), 
            new Currency("NPR", "Nepalese rupee"), 
            new Currency("ANG", "Netherlands Antillean Guilder"),
            new Currency("NIO", "Nicaraguan córdoba"),
            new Currency("PKR", "Pakistani rupee"), 
            new Currency("PGK", "Papua New Guinean kina"), 
            new Currency("PYG", "Paraguayan guaraní"), 
            new Currency("PEN", "Peruvian Nuevo Sol"),  
            new Currency("RWF", "Rwandan franc"), 
            new Currency("WST", "Samoan tala"), 
            new Currency("SLL", "Sierra Leonean leone"), 
            new Currency("SOS", "Somali shilling"), 
            new Currency("LKR", "Sri Lankan rupee"), 
            new Currency("SRD", "Surinamese dollar"), 
            new Currency("TZS", "Tanzanian shilling"), 
            new Currency("THB", "Thai baht"),
            new Currency("TOP", "Tongan paʻanga"), 
            new Currency("TND", "Tunisian dinar"), 
            new Currency("TRY", "Turkish lira"), 
            new Currency("UGX", "Ugandan shilling"), 
            new Currency("UYU", "Uruguayan peso"), 
            new Currency("VND", "Vietnamese dong"), 
            new Currency("ZMW", "Zambian kwacha"),
            new Currency("NGN", "Nigerian Naira")
        };
        #endregion

        #region Countries
        private static readonly IList<Country> OriginCountries = new[] {
            new Country("au", "Australia", Currencies.Single(x => x.Code == "AUD")),
            new Country("at", "Austria", Currencies.Single(x => x.Code == "EUR")),
            new Country("bh", "Bahrain", Currencies.Single(x => x.Code == "BHD")),
            new Country("be", "Belgium", Currencies.Single(x => x.Code == "EUR")),
            new Country("br", "Brazil", Currencies.Single(x => x.Code == "BRL")),
            new Country("bg", "Bulgaria", Currencies.Single(x => x.Code == "BGN")),
            new Country("ca", "Canada", Currencies.Single(x => x.Code == "CAD")),
            new Country("cy", "Cyprus", Currencies.Single(x => x.Code == "EUR")),
            new Country("cz", "Czech Republic", Currencies.Single(x => x.Code == "CZK")),
            new Country("dk", "Denmark", Currencies.Single(x => x.Code == "DKK")),
            new Country("ee", "Estonia", Currencies.Single(x => x.Code == "EUR")),
            new Country("fi", "Finland", Currencies.Single(x => x.Code == "EUR")),
            new Country("fr", "France", Currencies.Single(x => x.Code == "EUR")),
            new Country("de", "Germany", Currencies.Single(x => x.Code == "EUR")),
            new Country("gi", "Gibraltar", Currencies.Single(x => x.Code == "GIP")),
            new Country("gr", "Greece", Currencies.Single(x => x.Code == "GIP")),
            new Country("gu", "Guam", Currencies.Single(x => x.Code == "USD")),
            new Country("hk", "Hong Kong", Currencies.Single(x => x.Code == "HKD")),
            new Country("hu", "Hungary", Currencies.Single(x => x.Code == "HUF")),
            new Country("is", "Iceland", Currencies.Single(x => x.Code == "ISK")),
            new Country("ie", "Ireland", Currencies.Single(x => x.Code == "EUR")),
            new Country("it", "Italy", Currencies.Single(x => x.Code == "EUR")),
            new Country("jp", "Japan", Currencies.Single(x => x.Code == "JPY")),
            new Country("jo", "Jordan", Currencies.Single(x => x.Code == "JOD")),
            new Country("kw", "Kuwait", Currencies.Single(x => x.Code == "KWD")),
            new Country("lv", "Latvia", Currencies.Single(x => x.Code == "EUR")),
            new Country("lt", "Lithuania", Currencies.Single(x => x.Code == "EUR")),
            new Country("lu", "Luxembourg", Currencies.Single(x => x.Code == "EUR")),
            new Country("my", "Malaysia", Currencies.Single(x => x.Code == "MYR")),
            new Country("mt", "Malta", Currencies.Single(x => x.Code == "EUR")),
            new Country("nl", "Netherlands", Currencies.Single(x => x.Code == "EUR")),
            new Country("nz", "New Zealand", Currencies.Single(x => x.Code == "NZD")),
            new Country("no", "Norway", Currencies.Single(x => x.Code == "NOK")),
            new Country("om", "Oman", Currencies.Single(x => x.Code == "OMR")),
            new Country("ph", "Philippines", Currencies.Single(x => x.Code == "PHP")),
            new Country("pl", "Poland", Currencies.Single(x => x.Code == "PLN")),
            new Country("pt", "Portugal", Currencies.Single(x => x.Code == "EUR")),
            new Country("qa", "Qatar", Currencies.Single(x => x.Code == "QAR")),
            new Country("ro", "Romania", Currencies.Single(x => x.Code == "RON")),
            new Country("sa", "Saudi Arabia", Currencies.Single(x => x.Code == "SAR")),
            new Country("sg", "Singapore", Currencies.Single(x => x.Code == "SGD")),
            new Country("sk", "Slovakia", Currencies.Single(x => x.Code == "EUR")),
            new Country("si", "Slovenia", Currencies.Single(x => x.Code == "EUR")),
            new Country("za", "South Africa", Currencies.Single(x => x.Code == "ZAR")),
            new Country("kr", "Republic of Korea", Currencies.Single(x => x.Code == "KPW")),
            new Country("es", "Spain", Currencies.Single(x => x.Code == "EUR")),
            new Country("se", "Sweden", Currencies.Single(x => x.Code == "SEK")),
            new Country("ch", "Switzerland", Currencies.Single(x => x.Code == "CHF")),
            new Country("tw", "Taiwan", Currencies.Single(x => x.Code == "TWD")),
            new Country("ae", "United Arab Emirates", Currencies.Single(x => x.Code == "AED")),
            new Country("gb", "United Kingdom", Currencies.Single(x => x.Code == "GBP")),
            new Country("us", "United States", Currencies.Single(x => x.Code == "USD"))
        };

        private static readonly IList<Country> DestinationCountries = new[] {
            new Country("al", "Albania", Currencies.Single(x => x.Code == "ALL")),
            new Country("ai", "Anguilla", Currencies.Single(x => x.Code == "XCD")),
            new Country("ag", "Antigua and Barbuda", Currencies.Single(x => x.Code == "XCD")),
            new Country("ar", "Argentina", Currencies.Single(x => x.Code == "ARS")),
            new Country("am", "Armenia", Currencies.Single(x => x.Code == "AMD")),
            new Country("au", "Australia", Currencies.Single(x => x.Code == "AUD")),
            new Country("at", "Austria", Currencies.Single(x => x.Code == "EUR")),
            new Country("bd", "Bangladesh", Currencies.Single(x => x.Code == "BDT")),
            new Country("bb", "Barbados", Currencies.Single(x => x.Code == "BBD")),
            new Country("be", "Belgium", Currencies.Single(x => x.Code == "EUR")),
            new Country("bj", "Benin", Currencies.Single(x => x.Code == "XOF")),
            new Country("bo", "Bolivia", Currencies.Single(x => x.Code == "BOB")),
            new Country("ba", "Bosnia and Herzegovina", Currencies.Single(x => x.Code == "BAM")),
            new Country("br", "Brazil", Currencies.Single(x => x.Code == "BRL")),
            new Country("vg", "British Virgin Islands", Currencies.Single(x => x.Code == "USD")),
            new Country("bg", "Bulgaria", Currencies.Single(x => x.Code == "BGN")),
            new Country("bf", "Burkina Faso", Currencies.Single(x => x.Code == "XOF")),
            new Country("bi", "Burundi", Currencies.Single(x => x.Code == "BIF")),
            new Country("kh", "Cambodia", Currencies.Single(x => x.Code == "KHR")),
            new Country("cm", "Cameroon", Currencies.Single(x => x.Code == "XAF")),
            new Country("cv", "Cape Verde", Currencies.Single(x => x.Code == "CVE")),
            new Country("ky", "Cayman Islands", Currencies.Single(x => x.Code == "KYD")),
            new Country("cf", "Central African Republic", Currencies.Single(x => x.Code == "XAF")),
            new Country("td", "Chad", Currencies.Single(x => x.Code == "XAF")),
            new Country("cl", "Chile", Currencies.Single(x => x.Code == "CLP")),
            new Country("cn", "China", Currencies.Single(x => x.Code == "CNY")),
            new Country("co", "Colombia", Currencies.Single(x => x.Code == "COP")),
            new Country("km", "Comoros", Currencies.Single(x => x.Code == "KMF")),
            new Country("cg", "Congo Brazza", Currencies.Single(x => x.Code == "XAF")),
            new Country("cd", "Congo DRC", Currencies.Single(x => x.Code == "CDF")),
            new Country("cr", "Costa Rica", Currencies.Single(x => x.Code == "CRC")),
            new Country("hr", "Croatia", Currencies.Single(x => x.Code == "HRK")),
            new Country("cu", "Cuba", Currencies.Single(x => x.Code == "CUP")),
            new Country("cz", "Czech Republic", Currencies.Single(x => x.Code == "CZK")),
            new Country("dk", "Denmark", Currencies.Single(x => x.Code == "DKK")),
            new Country("dm", "Dominica", Currencies.Single(x => x.Code == "XCD")),
            new Country("do", "Dominican Republic", Currencies.Single(x => x.Code == "DOP")),
            new Country("ec", "Ecuador", Currencies.Single(x => x.Code == "USD")),
            new Country("eg", "Egypt", Currencies.Single(x => x.Code == "EGP")),
            new Country("sv", "El Salvador", Currencies.Single(x => x.Code == "USD")),
            new Country("et", "Ethiopia", Currencies.Single(x => x.Code == "ETB")),
            new Country("fj", "Fiji", Currencies.Single(x => x.Code == "FJD")),
            new Country("fi", "Finland", Currencies.Single(x => x.Code == "EUR")),
            new Country("fr", "France", Currencies.Single(x => x.Code == "EUR")),
            new Country("ga", "Gabon", Currencies.Single(x => x.Code == "XAF")),
            new Country("gm", "Gambia", Currencies.Single(x => x.Code == "GMD")),
            new Country("de", "Germany", Currencies.Single(x => x.Code == "EUR")),
            new Country("gh", "Ghana", Currencies.Single(x => x.Code == "GHS")),
            new Country("gr", "Greece", Currencies.Single(x => x.Code == "EUR")),
            new Country("gd", "Grenada", Currencies.Single(x => x.Code == "XCD")),
            new Country("gt", "Guatemala", Currencies.Single(x => x.Code == "GTQ")),
            new Country("gw", "Guinea Bissau", Currencies.Single(x => x.Code == "XOF")),
            new Country("gn", "Guinea-Conakry", Currencies.Single(x => x.Code == "GNF")),
            new Country("ht", "Haiti", Currencies.Single(x => x.Code == "HTG")),
            new Country("hn", "Honduras", Currencies.Single(x => x.Code == "HNL")),
            new Country("hk", "Hong Kong", Currencies.Single(x => x.Code == "HKD")),
            new Country("hu", "Hungary", Currencies.Single(x => x.Code == "HUF")),
            new Country("in", "India", Currencies.Single(x => x.Code == "INR")),
            new Country("id", "Indonesia", Currencies.Single(x => x.Code == "IDR")),
            new Country("ie", "Ireland", Currencies.Single(x => x.Code == "EUR")),
            new Country("il", "Israel", Currencies.Single(x => x.Code == "ILS")),
            new Country("it", "Italy", Currencies.Single(x => x.Code == "EUR")),
            new Country("ci", "Ivory Coast", Currencies.Single(x => x.Code == "CFA")),
            new Country("jm", "Jamaica", Currencies.Single(x => x.Code == "JMD")),
            new Country("jo", "Jordan", Currencies.Single(x => x.Code == "JOD")),
            new Country("ke", "Kenya", Currencies.Single(x => x.Code == "KES")),
            new Country("la", "Laos", Currencies.Single(x => x.Code == "LAK")),
            new Country("lv", "Latvia", Currencies.Single(x => x.Code == "EUR")),
            new Country("ls", "Lesotho", Currencies.Single(x => x.Code == "LSL")),
            new Country("lr", "Liberia", Currencies.Single(x => x.Code == "LRD")),
            new Country("lt", "Lithuania", Currencies.Single(x => x.Code == "EUR")),
            new Country("mg", "Madagascar", Currencies.Single(x => x.Code == "MGA")),
            new Country("my", "Malaysia", Currencies.Single(x => x.Code == "MYR")),
            new Country("ml", "Mali", Currencies.Single(x => x.Code == "XOF")),
            new Country("mt", "Malta", Currencies.Single(x => x.Code == "EUR")),
            new Country("mr", "Mauritania", Currencies.Single(x => x.Code == "MRO")),
            new Country("mx", "Mexico", Currencies.Single(x => x.Code == "MXN")),
            new Country("ms", "Montserrat", Currencies.Single(x => x.Code == "XCD")),
            new Country("ma", "Morocco", Currencies.Single(x => x.Code == "MAD")),
            new Country("mz", "Mozambique", Currencies.Single(x => x.Code == "MZN")),
            new Country("np", "Nepal", Currencies.Single(x => x.Code == "NPR")),
            new Country("nl", "Netherlands", Currencies.Single(x => x.Code == "EUR")),
            new Country("an", "Netherlands Antilles", Currencies.Single(x => x.Code == "ANG")),
            new Country("nz", "New Zealand", Currencies.Single(x => x.Code == "NZD")),
            new Country("ni", "Nicaragua", Currencies.Single(x => x.Code == "NIO")),
            new Country("ne", "Niger", Currencies.Single(x => x.Code == "XOF")),
            new Country("ng", "Nigeria", Currencies.Single(x => x.Code == "NGN")),
            new Country("no", "Norway", Currencies.Single(x => x.Code == "NOK")),
            new Country("pk", "Pakistan", Currencies.Single(x => x.Code == "PKR")),
            new Country("pa", "Panama", Currencies.Single(x => x.Code == "USD")),
            new Country("pg", "Papua New Guinea", Currencies.Single(x => x.Code == "PGK")),
            new Country("py", "Paraguay", Currencies.Single(x => x.Code == "PYG")),
            new Country("pe", "Peru", Currencies.Single(x => x.Code == "PEN")),
            new Country("ph", "Philippines", Currencies.Single(x => x.Code == "PHP")),
            new Country("pl", "Poland", Currencies.Single(x => x.Code == "PLN")),
            new Country("pt", "Portugal", Currencies.Single(x => x.Code == "EUR")),
            new Country("pr", "Puerto Rico", Currencies.Single(x => x.Code == "USD")),
            new Country("ro", "Romania", Currencies.Single(x => x.Code == "RON")),
            new Country("rw", "Rwanda", Currencies.Single(x => x.Code == "RWF")),
            new Country("ws", "Samoa", Currencies.Single(x => x.Code == "WST")),
            new Country("sn", "Senegal", Currencies.Single(x => x.Code == "XOF")),
            new Country("sl", "Sierra Leone", Currencies.Single(x => x.Code == "SLL")),
            new Country("sg", "Singapore", Currencies.Single(x => x.Code == "SGD")),
            new Country("sk", "Slovakia", Currencies.Single(x => x.Code == "EUR")),
            new Country("si", "Slovenia", Currencies.Single(x => x.Code == "EUR")),
            new Country("ss", "Somaliland", Currencies.Single(x => x.Code == "SOS")),
            new Country("za", "South Africa", Currencies.Single(x => x.Code == "ZAR")),
            new Country("es", "Spain", Currencies.Single(x => x.Code == "EUR")),
            new Country("lk", "Sri Lanka", Currencies.Single(x => x.Code == "LKR")),
            new Country("kn", "St Kitts and Nevis", Currencies.Single(x => x.Code == "XCD")),
            new Country("lc", "St Lucia", Currencies.Single(x => x.Code == "XCD")),
            new Country("vc", "St Vincent Grenadines", Currencies.Single(x => x.Code == "XCD")),
            new Country("sr", "Suriname", Currencies.Single(x => x.Code == "SRD")),
            new Country("se", "Sweden", Currencies.Single(x => x.Code == "SEK")),
            new Country("ch", "Switzerland", Currencies.Single(x => x.Code == "CHF")),
            new Country("tz", "Tanzania", Currencies.Single(x => x.Code == "TZS")),
            new Country("th", "Thailand", Currencies.Single(x => x.Code == "THB")),
            new Country("tg", "Togo", Currencies.Single(x => x.Code == "XOF")),
            new Country("to", "Tonga", Currencies.Single(x => x.Code == "TOP")),
            new Country("tn", "Tunisia", Currencies.Single(x => x.Code == "TND")),
            new Country("tr", "Turkey", Currencies.Single(x => x.Code == "TRY")),
            new Country("tc", "Turks and Caicos", Currencies.Single(x => x.Code == "USD")),
            new Country("ug", "Uganda", Currencies.Single(x => x.Code == "UGX")),
            new Country("ae", "United Arab Emirates", Currencies.Single(x => x.Code == "AED")),
            new Country("gb", "United Kingdom", Currencies.Single(x => x.Code == "GBP")),
            new Country("us", "United States", Currencies.Single(x => x.Code == "USD")),
            new Country("uy", "Uruguay", Currencies.Single(x => x.Code == "UYU")),
            new Country("vn", "Vietnam", Currencies.Single(x => x.Code == "VND")),
            new Country("zm", "Zambia", Currencies.Single(x => x.Code == "ZMW")),
            new Country("zw", "Zimbabwe", Currencies.Single(x => x.Code == "USD"))
        };
        #endregion

        #region DeliveryMethodTypes
        private static readonly IList<DeliveryMethodType> DeliveryMethodTypes = new[] {
            new DeliveryMethodType("bnk", "Bank Transfer"),
            new DeliveryMethodType("csh", "Cash Pickup"),
            new DeliveryMethodType("mob", "Mobile Money Transfer"),
            new DeliveryMethodType("dtd", "Door to Door Delivery")
        };
        #endregion

        #region PayoutNetworks
        private static readonly IDictionary<string, List<PayoutNetwork>> PayoutNetworks = new Dictionary<string, List<PayoutNetwork>> {
            {
                "bnk",
                new List<PayoutNetwork>
                {
                    new PayoutNetwork("Bank 1"),
                    new PayoutNetwork("Bank 2"),
                    new PayoutNetwork("Bank 3"),
                    new PayoutNetwork("Bank 4"),
                    new PayoutNetwork("Bank 5")
                }
            },
            {
                "csh",
                new List<PayoutNetwork>
                {
                    new PayoutNetwork("Cash Pickup Provider 1"),
                    new PayoutNetwork("Cash Pickup Provider 2"),
                    new PayoutNetwork("Cash Pickup Provider 3"),
                    new PayoutNetwork("Cash Pickup Provider 4"),
                    new PayoutNetwork("Cash Pickup Provider 5")
                }
            },
            {
                "mob",
                new List<PayoutNetwork>
                {
                    new PayoutNetwork("Mobile Money Transfer Provider 1"),
                    new PayoutNetwork("Mobile Money Transfer Provider 2"),
                    new PayoutNetwork("Mobile Money Transfer Provider 3"),
                    new PayoutNetwork("Mobile Money Transfer Provider 4"),
                    new PayoutNetwork("Mobile Money Transfer Provider 5")
                }
            }
        };
        #endregion

        public static IList<Corridor> Create()
        {
            var destinationCountryRandomizer = new Random();
            var deliveryMethodTypesRandomizer = new Random();
            var payoutNetworksRandomizer = new Random();
            var exchangeRateRandomizer = new Random();
            var fixedFeeRandomizer = new Random();

            var corridors = new List<Corridor>();

            foreach (var originCountry in OriginCountries)
            {
                for (var destinationCountryCount = 0; destinationCountryCount < 20; destinationCountryCount++)
                {
                    var destinationCountry = DestinationCountries[destinationCountryRandomizer.Next(0, DestinationCountries.Count)];
                    while (corridors.Any(x => x.Origin.Code == originCountry.Code && x.Destination.Code == destinationCountry.Code))
                        destinationCountry = DestinationCountries[destinationCountryRandomizer.Next(0, DestinationCountries.Count)];

                    var exchangeRate = exchangeRateRandomizer.NextPercentage();
                    var fixedFee = fixedFeeRandomizer.NextDecimal(0, 5);
                    var corridor = new Corridor(originCountry, destinationCountry, exchangeRate, fixedFee);

                    var numberOfDeliveryMethodTypes = deliveryMethodTypesRandomizer.Next(1, DeliveryMethodTypes.Count);
                    for (var deliveryMethodTypesCount = 0; deliveryMethodTypesCount < numberOfDeliveryMethodTypes; deliveryMethodTypesCount++)
                    {
                        var deliveryMethodType = DeliveryMethodTypes[deliveryMethodTypesCount];
                        var payoutNetworks = new List<PayoutNetwork>();

                        if (PayoutNetworks.ContainsKey(deliveryMethodType.Code))
                            payoutNetworks = PayoutNetworks[deliveryMethodType.Code];

                        var deliveryMethod = new DeliveryMethod(deliveryMethodType);

                        if (payoutNetworks.Any())
                        {
                            var numberOfPayoutNetworks = payoutNetworksRandomizer.Next(1, payoutNetworks.Count);
                            for (var payoutNetworksCount = 0; payoutNetworksCount < numberOfPayoutNetworks; payoutNetworksCount++)
                                deliveryMethod.AddPayoutNetwork(payoutNetworks[payoutNetworksCount]);
                        }

                        corridor.AddDeliveryMethod(deliveryMethod);
                    };

                    corridors.Add(corridor);
                }
            }

            return corridors;
        }
    }

    public static class RandomExtensions
    {
        public static decimal NextPercentage(this Random rng)
        {
            return rng.Next(0, 100) / 100m;
        }

        public static decimal NextDecimal(this Random rng, int min, int max)
        {
            return rng.Next(min * 100, max * 100) / 100m;
        }
    }
}
