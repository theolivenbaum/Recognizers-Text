# ------------------------------------------------------------------------------
# <auto-generated>
#     This code was generated by a tool.
#     Changes to this file may cause incorrect behavior and will be lost if
#     the code is regenerated.
# </auto-generated>
#
# Copyright (c) Microsoft Corporation. All rights reserved.
# Licensed under the MIT License.
# ------------------------------------------------------------------------------

from .base_date_time import BaseDateTime
# pylint: disable=line-too-long


class TimeZoneDefinitions:
    DirectUtcRegex = f'\\b(utc|gmt)(\\s*[+\\-\\u00B1]?\\s*[\\d]{{1,2}}(\\s*:\\s*[\\d]{{1,2}})?)?\\b'
    AbbreviationsList = [r'ABST', r'ACDT', r'ACST', r'ACT', r'ADT', r'AEDT', r'AEST', r'AET', r'AFT', r'AKDT', r'AKST', r'AMST', r'AMT', r'ARBST', r'ARST', r'ART', r'AST', r'AWDT', r'AWST', r'AZOST', r'AZOT', r'AZST', r'AZT', r'BIT', r'BST', r'BTT', r'CADT', r'CAST', r'CBST', r'CBT', r'CCST', r'CDT', r'CDTM', r'CEST', r'CET', r'COT', r'CST', r'CSTM', r'CT', r'CVT', r'EAT', r'ECT', r'EDT', r'EDTM', r'EEST', r'EET', r'EGST', r'ESAST', r'ESAT', r'EST', r'ESTM', r'ET', r'FJST', r'FJT', r'GET', r'GMT', r'GNDT', r'GNST', r'GST', r'GTBST', r'HADT', r'HAST', r'HDT', r'HKT', r'HST', r'IRDT', r'IRKT', r'IRST', r'ISDT', r'ISST', r'IST', r'JDT', r'JST', r'KRAT', r'KST', r'LINT', r'MAGST', r'MAGT', r'MAT', r'MDT', r'MDTM', r'MEST', r'MOST', r'MSK', r'MSK+1', r'MSK+2', r'MSK+3', r'MSK+4', r'MSK+5', r'MSK+6', r'MSK+7', r'MSK+8', r'MSK+9', r'MSK-1', r'MST', r'MSTM', r'MUT', r'MVST', r'MYST', r'NCAST', r'NDT', r'NMDT', r'NMST', r'NPT', r'NST', r'NZDT', r'NZST', r'NZT', r'PDST', r'PDT', r'PDTM', r'PETT', r'PKT', r'PSAST', r'PSAT', r'PST', r'PSTM', r'PT', r'PYST', r'PYT', r'RST', r'SAEST', r'SAPST', r'SAST', r'SAWST', r'SBT', r'SGT', r'SLT', r'SMST', r'SNST', r'SST', r'TADT', r'TAST', r'THA', r'TIST', r'TOST', r'TOT', r'TRT', r'TST', r'ULAT', r'UTC', r'VET', r'VLAT', r'WAST', r'WAT', r'WEST', r'WET', r'WPST', r'YAKT', r'YEKT']
    FullNameList = [r'Acre Time', r'Afghanistan Standard Time', r'Alaskan Standard Time', r'Arab Standard Time', r'Arabian Standard Time', r'Arabic Standard Time', r'Argentina Standard Time', r'Atlantic Standard Time', r'AUS Central Standard Time', r'Australian Central Time', r'AUS Eastern Standard Time', r'Australian Eastern Time', r'Australian Eastern Standard Time', r'Australian Central Daylight Time', r'Australian Eastern Daylight Time', r'Azerbaijan Standard Time', r'Azores Standard Time', r'Bahia Standard Time', r'Bangladesh Standard Time', r'Belarus Standard Time', r'Canada Central Standard Time', r'Cape Verde Standard Time', r'Caucasus Standard Time', r'Cen. Australia Standard Time', r'Central America Standard Time', r'Central Asia Standard Time', r'Central Brazilian Standard Time', r'Central Daylight Time', r'Europe Central Time', r'European Central Time', r'Central Europe Standard Time', r'Central Europe Std Time', r'Central European Std Time', r'Central European Standard Time', r'Central Pacific Standard Time', r'Central Standard Time', r'Central Standard Time (Mexico)', r'China Standard Time', r'Dateline Standard Time', r'E. Africa Standard Time', r'E. Australia Standard Time', r'E. Europe Standard Time', r'E. South America Standard Time', r'Eastern Time', r'Eastern Daylight Time', r'Eastern Standard Time', r'Eastern Standard Time (Mexico)', r'Egypt Standard Time', r'Ekaterinburg Standard Time', r'Fiji Standard Time', r'FLE Standard Time', r'Georgian Standard Time', r'GMT Standard Time', r'Greenland Standard Time', r'Greenwich Standard Time', r'GTB Standard Time', r'Hawaiian Standard Time', r'India Standard Time', r'Iran Standard Time', r'Israel Standard Time', r'Jordan Standard Time', r'Kaliningrad Standard Time', r'Kamchatka Standard Time', r'Korea Standard Time', r'Libya Standard Time', r'Line Islands Standard Time', r'Magadan Standard Time', r'Mauritius Standard Time', r'Mid-Atlantic Standard Time', r'Middle East Standard Time', r'Montevideo Standard Time', r'Morocco Standard Time', r'Mountain Standard Time', r'Mountain Standard Time (Mexico)', r'Myanmar Standard Time', r'N. Central Asia Standard Time', r'Namibia Standard Time', r'Nepal Standard Time', r'New Zealand Standard Time', r'Newfoundland Standard Time', r'North Asia East Standard Time', r'North Asia Standard Time', r'North Korea Standard Time', r'Pacific SA Standard Time', r'Pacific Standard Time', r'Pacific Daylight Time', r'Pacific Time', r'Pacific Standard Time', r'Pacific Standard Time (Mexico)', r'Pakistan Standard Time', r'Paraguay Standard Time', r'Romance Standard Time', r'Russia Time Zone 1', r'Russia Time Zone 2', r'Russia Time Zone 3', r'Russia Time Zone 4', r'Russia Time Zone 5', r'Russia Time Zone 6', r'Russia Time Zone 7', r'Russia Time Zone 8', r'Russia Time Zone 9', r'Russia Time Zone 10', r'Russia Time Zone 11', r'Russian Standard Time', r'SA Eastern Standard Time', r'SA Pacific Standard Time', r'SA Western Standard Time', r'Samoa Standard Time', r'SE Asia Standard Time', r'Singapore Standard Time', r'Singapore Time', r'South Africa Standard Time', r'Sri Lanka Standard Time', r'Syria Standard Time', r'Taipei Standard Time', r'Tasmania Standard Time', r'Tokyo Standard Time', r'Tonga Standard Time', r'Turkey Standard Time', r'Ulaanbaatar Standard Time', r'US Eastern Standard Time', r'US Mountain Standard Time', r'Mountain', r'Venezuela Standard Time', r'Vladivostok Standard Time', r'W. Australia Standard Time', r'W. Central Africa Standard Time', r'W. Europe Standard Time', r'West Asia Standard Time', r'West Pacific Standard Time', r'Yakutsk Standard Time', r'Pacific Daylight Saving Time', r'Austrialian Western Daylight Time', r'Austrialian West Daylight Time', r'Australian Western Daylight Time', r'Australian West Daylight Time', r'Colombia Time', r'Hong Kong Time', r'Central Europe Time', r'Central European Time', r'Central Europe Summer Time', r'Central European Summer Time', r'Central Europe Standard Time', r'Central European Standard Time', r'Central Europe Std Time', r'Central European Std Time', r'West Coast Time', r'West Coast', r'Central Time', r'Central', r'Pacific', r'Eastern']
    LocationTimeSuffixRegex = f'((\\s+|-)(timezone|time)\\b)'
    AmbiguousTimezoneList = [r'bit', r'get', r'art', r'cast', r'eat', r'lint', r'mat', r'most', r'west', r'vet', r'wet', r'cot', r'pt', r'et', r'eastern', r'pacific', r'central', r'mountain', r'west coast']
    AbbrToMinMapping = dict([("abst", 180),
                             ("acdt", 630),
                             ("acst", 570),
                             ("act", -10000),
                             ("adt", -10000),
                             ("aedt", 660),
                             ("aest", 600),
                             ("aet", 600),
                             ("aft", 270),
                             ("akdt", -480),
                             ("akst", -540),
                             ("amst", -10000),
                             ("amt", -10000),
                             ("arbst", 180),
                             ("arst", 180),
                             ("art", -180),
                             ("ast", -10000),
                             ("awdt", 540),
                             ("awst", 480),
                             ("azost", 0),
                             ("azot", -60),
                             ("azst", 300),
                             ("azt", 240),
                             ("bit", -720),
                             ("bst", -10000),
                             ("btt", 360),
                             ("cadt", -360),
                             ("cast", 480),
                             ("cbst", -240),
                             ("cbt", -240),
                             ("ccst", -360),
                             ("cdt", -10000),
                             ("cdtm", -360),
                             ("cest", 120),
                             ("cet", 60),
                             ("cot", -300),
                             ("cst", -10000),
                             ("cstm", -360),
                             ("ct", -360),
                             ("cvt", -60),
                             ("eat", 180),
                             ("ect", -10000),
                             ("edt", -240),
                             ("edtm", -300),
                             ("eest", 180),
                             ("eet", 120),
                             ("egst", 0),
                             ("esast", -180),
                             ("esat", -180),
                             ("est", -300),
                             ("estm", -300),
                             ("et", -240),
                             ("fjst", 780),
                             ("fjt", 720),
                             ("get", 240),
                             ("gmt", 0),
                             ("gndt", -180),
                             ("gnst", -180),
                             ("gst", -10000),
                             ("gtbst", 120),
                             ("hadt", -540),
                             ("hast", -600),
                             ("hdt", -540),
                             ("hkt", 480),
                             ("hst", -600),
                             ("irdt", 270),
                             ("irkt", 480),
                             ("irst", 210),
                             ("isdt", 120),
                             ("isst", 120),
                             ("ist", -10000),
                             ("jdt", 120),
                             ("jst", 540),
                             ("krat", 420),
                             ("kst", -10000),
                             ("lint", 840),
                             ("magst", 720),
                             ("magt", 660),
                             ("mat", -120),
                             ("mdt", -360),
                             ("mdtm", -420),
                             ("mest", 120),
                             ("most", 0),
                             ("msk+1", 240),
                             ("msk+2", 300),
                             ("msk+3", 360),
                             ("msk+4", 420),
                             ("msk+5", 480),
                             ("msk+6", 540),
                             ("msk+7", 600),
                             ("msk+8", 660),
                             ("msk+9", 720),
                             ("msk-1", 120),
                             ("msk", 180),
                             ("mst", -420),
                             ("mstm", -420),
                             ("mut", 240),
                             ("mvst", -180),
                             ("myst", 390),
                             ("ncast", 420),
                             ("ndt", -150),
                             ("nmdt", 60),
                             ("nmst", 60),
                             ("npt", 345),
                             ("nst", -210),
                             ("nzdt", 780),
                             ("nzst", 720),
                             ("nzt", 720),
                             ("pdst", -420),
                             ("pdt", -420),
                             ("pdtm", -480),
                             ("pett", 720),
                             ("pkt", 300),
                             ("psast", -240),
                             ("psat", -240),
                             ("pst", -480),
                             ("pstm", -480),
                             ("pt", -420),
                             ("pyst", -10000),
                             ("pyt", -10000),
                             ("rst", 60),
                             ("saest", -180),
                             ("sapst", -300),
                             ("sast", 120),
                             ("sawst", -240),
                             ("sbt", 660),
                             ("sgt", 480),
                             ("slt", 330),
                             ("smst", 780),
                             ("snst", 480),
                             ("sst", -10000),
                             ("tadt", 600),
                             ("tast", 600),
                             ("tha", 420),
                             ("tist", 480),
                             ("tost", 840),
                             ("tot", 780),
                             ("trt", 180),
                             ("tst", 540),
                             ("ulat", 480),
                             ("utc", 0),
                             ("vet", -240),
                             ("vlat", 600),
                             ("wast", 120),
                             ("wat", -10000),
                             ("west", 60),
                             ("wet", 0),
                             ("wpst", 600),
                             ("yakt", 540),
                             ("yekt", 300)])
    FullToMinMapping = dict([("beijing", 480),
                             ("shanghai", 480),
                             ("shenzhen", 480),
                             ("suzhou", 480),
                             ("tianjian", 480),
                             ("chengdu", 480),
                             ("guangzhou", 480),
                             ("wuxi", 480),
                             ("xiamen", 480),
                             ("chongqing", 480),
                             ("shenyang", 480),
                             ("china", 480),
                             ("redmond", -480),
                             ("seattle", -480),
                             ("bellevue", -480),
                             ("pacific daylight", -420),
                             ("pacific", -480),
                             ("afghanistan standard", 270),
                             ("alaskan standard", -540),
                             ("arab standard", 180),
                             ("arabian standard", 180),
                             ("arabic standard", 180),
                             ("argentina standard", -180),
                             ("atlantic standard", -240),
                             ("aus central standard", 570),
                             ("aus eastern standard", 600),
                             ("australian eastern", 600),
                             ("australian eastern standard", 600),
                             ("australian central daylight", 630),
                             ("australian eastern daylight", 660),
                             ("azerbaijan standard", 240),
                             ("azores standard", -60),
                             ("bahia standard", -180),
                             ("bangladesh standard", 360),
                             ("belarus standard", 180),
                             ("canada central standard", -360),
                             ("cape verde standard", -60),
                             ("caucasus standard", 240),
                             ("cen. australia standard", 570),
                             ("central australia standard", 570),
                             ("central america standard", -360),
                             ("central asia standard", 360),
                             ("central brazilian standard", -240),
                             ("central daylight", -10000),
                             ("central europe", 60),
                             ("central european", 60),
                             ("central europe std", 60),
                             ("central european std", 60),
                             ("central europe standard", 60),
                             ("central european standard", 60),
                             ("central europe summer", 120),
                             ("central european summer", 120),
                             ("central pacific standard", 660),
                             ("central standard time (mexico)", -360),
                             ("central standard", -360),
                             ("china standard", 480),
                             ("dateline standard", -720),
                             ("e. africa standard", 180),
                             ("e. australia standard", 600),
                             ("e. europe standard", 120),
                             ("e. south america standard", -180),
                             ("europe central", 60),
                             ("european central", 60),
                             ("central", -300),
                             ("eastern", -240),
                             ("eastern daylight", -10000),
                             ("eastern standard time (mexico)", -300),
                             ("eastern standard", -300),
                             ("egypt standard", 120),
                             ("ekaterinburg standard", 300),
                             ("fiji standard", 720),
                             ("fle standard", 120),
                             ("georgian standard", 240),
                             ("gmt standard", 0),
                             ("greenland standard", -180),
                             ("greenwich standard", 0),
                             ("gtb standard", 120),
                             ("hawaiian standard", -600),
                             ("india standard", 330),
                             ("iran standard", 210),
                             ("israel standard", 120),
                             ("jordan standard", 120),
                             ("kaliningrad standard", 120),
                             ("kamchatka standard", 720),
                             ("korea standard", 540),
                             ("libya standard", 120),
                             ("line islands standard", 840),
                             ("magadan standard", 660),
                             ("mauritius standard", 240),
                             ("mid-atlantic standard", -120),
                             ("middle east standard", 120),
                             ("montevideo standard", -180),
                             ("morocco standard", 0),
                             ("mountain", -360),
                             ("mountain standard", -420),
                             ("mountain standard time (mexico)", -420),
                             ("myanmar standard", 390),
                             ("n. central asia standard", 420),
                             ("namibia standard", 60),
                             ("nepal standard", 345),
                             ("new zealand standard", 720),
                             ("newfoundland standard", -210),
                             ("north asia east standard", 480),
                             ("north asia standard", 420),
                             ("north korea standard", 510),
                             ("west coast", -420),
                             ("pacific sa standard", -240),
                             ("pacific standard", -480),
                             ("pacific standard time (mexico)", -480),
                             ("pakistan standard", 300),
                             ("paraguay standard", -240),
                             ("romance standard", 60),
                             ("russia time zone 1", 120),
                             ("russia time zone 2", 180),
                             ("russia time zone 3", 240),
                             ("russia time zone 4", 300),
                             ("russia time zone 5", 360),
                             ("russia time zone 6", 420),
                             ("russia time zone 7", 480),
                             ("russia time zone 8", 540),
                             ("russia time zone 9", 600),
                             ("russia time zone 10", 660),
                             ("russia time zone 11", 720),
                             ("russian standard", 180),
                             ("sa eastern standard", -180),
                             ("sa pacific standard", -300),
                             ("sa western standard", -240),
                             ("samoa standard", -660),
                             ("se asia standard", 420),
                             ("singapore standard", 480),
                             ("singapore", 480),
                             ("south africa standard", 120),
                             ("sri lanka standard", 330),
                             ("syria standard", 120),
                             ("taipei standard", 480),
                             ("tasmania standard", 600),
                             ("tokyo standard", 540),
                             ("tonga standard", 780),
                             ("turkey standard", 180),
                             ("ulaanbaatar standard", 480),
                             ("us eastern standard", -300),
                             ("us mountain standard", -420),
                             ("venezuela standard", -240),
                             ("vladivostok standard", 600),
                             ("w. australia standard", 480),
                             ("w. central africa standard", 60),
                             ("w. europe standard", 0),
                             ("western european", 0),
                             ("west europe standard", 0),
                             ("west europe std", 0),
                             ("western europe standard", 0),
                             ("western europe summer", 60),
                             ("w. europe summer", 60),
                             ("western european summer", 60),
                             ("west europe summer", 60),
                             ("west asia standard", 300),
                             ("west pacific standard", 600),
                             ("yakutsk standard", 540),
                             ("pacific daylight saving", -420),
                             ("australian western daylight", 540),
                             ("australian west daylight", 540),
                             ("austrialian western daylight", 540),
                             ("austrialian west daylight", 540),
                             ("colombia", -300),
                             ("hong kong", 480),
                             ("madrid", 60),
                             ("bilbao", 60),
                             ("seville", 60),
                             ("valencia", 60),
                             ("malaga", 60),
                             ("las Palmas", 60),
                             ("zaragoza", 60),
                             ("alicante", 60),
                             ("alche", 60),
                             ("oviedo", 60),
                             ("gijón", 60),
                             ("avilés", 60)])
    MajorLocations = [r'Dominican Republic', r'Dominica', r'Guinea Bissau', r'Guinea-Bissau', r'Guinea', r'Equatorial Guinea', r'Papua New Guinea', r'New York City', r'New York', r'York', r'Mexico City', r'New Mexico', r'Mexico', r'Aberdeen', r'Adelaide', r'Anaheim', r'Atlanta', r'Auckland', r'Austin', r'Bangkok', r'Baltimore', r'Baton Rouge', r'Beijing', r'Belfast', r'Birmingham', r'Bolton', r'Boston', r'Bournemouth', r'Bradford', r'Brisbane', r'Bristol', r'Calgary', r'Canberra', r'Cardiff', r'Charlotte', r'Chicago', r'Christchurch', r'Colchester', r'Colorado Springs', r'Coventry', r'Dallas', r'Denver', r'Derby', r'Detroit', r'Dubai', r'Dublin', r'Dudley', r'Dunedin', r'Edinburgh', r'Edmonton', r'El Paso', r'Glasgow', r'Gold Coast', r'Hamilton', r'Hialeah', r'Houston', r'Ipswich', r'Jacksonville', r'Jersey City', r'Kansas City', r'Kingston-upon-Hull', r'Leeds', r'Leicester', r'Lexington', r'Lincoln', r'Liverpool', r'London', r'Long Beach', r'Los Angeles', r'Louisville', r'Lubbock', r'Luton', r'Madison', r'Manchester', r'Mansfield', r'Melbourne', r'Memphis', r'Mesa', r'Miami', r'Middlesbrough', r'Milan', r'Milton Keynes', r'Minneapolis', r'Montréal', r'Montreal', r'Nashville', r'New Orleans', r'Newark', r'Newcastle-upon-Tyne', r'Newcastle', r'Northampton', r'Norwich', r'Nottingham', r'Oklahoma City', r'Oldham', r'Omaha', r'Orlando', r'Ottawa', r'Perth', r'Peterborough', r'Philadelphia', r'Phoenix', r'Plymouth', r'Portland', r'Portsmouth', r'Preston', r'Québec City', r'Quebec City', r'Québec', r'Quebec', r'Raleigh', r'Reading', r'Redmond', r'Richmond', r'Rome', r'San Antonio', r'San Diego', r'San Francisco', r'San José', r'Santa Ana', r'Seattle', r'Sheffield', r'Southampton', r'Southend-on-Sea', r'Spokane', r'St Louis', r'St Paul', r'St Petersburg', r'St. Louis', r'St. Paul', r'St. Petersburg', r'Stockton-on-Tees', r'Stockton', r'Stoke-on-Trent', r'Sunderland', r'Swansea', r'Swindon', r'Sydney', r'Tampa', r'Tauranga', r'Telford', r'Toronto', r'Vancouver', r'Virginia Beach', r'Walsall', r'Warrington', r'Washington', r'Wellington', r'Wolverhampton', r'Abilene', r'Akron', r'Albuquerque', r'Alexandria', r'Allentown', r'Amarillo', r'Anchorage', r'Ann Arbor', r'Antioch', r'Arlington', r'Arvada', r'Athens', r'Augusta', r'Aurora', r'Bakersfield', r'Beaumont', r'Bellevue', r'Berkeley', r'Billings', r'Boise', r'Boulder', r'Bridgeport', r'Broken Arrow', r'Brownsville', r'Buffalo', r'Burbank', r'Cambridge', r'Cape Coral', r'Carlsbad', r'Carrollton', r'Cary', r'Cedar Rapids', r'Centennial', r'Chandler', r'Charleston', r'Chattanooga', r'Chengdu', r'Chesapeake', r'Chongqing', r'Chula Vista', r'Cincinnati', r'Clarksville', r'Clearwater', r'Cleveland', r'Clovis', r'College Station', r'Columbia', r'Columbus', r'Concord', r'Coral Springs', r'Corona', r'Costa Mesa', r'Daly City', r'Davenport', r'Dayton', r'Denton', r'Des Moines', r'Downey', r'Durham', r'Edison', r'El Cajon', r'El Monte', r'Elgin', r'Elizabeth', r'Elk Grove', r'Erie', r'Escondido', r'Eugene', r'Evansville', r'Everett', r'Fairfield', r'Fargo', r'Farmington Hills', r'Fayetteville', r'Fontana', r'Fort Collins', r'Fort Lauderdale', r'Fort Wayne', r'Fort Worth', r'Fremont', r'Fresno', r'Frisco', r'Fullerton', r'Gainesville', r'Garden Grove', r'Garland', r'Gilbert', r'Glendale', r'Grand Prairie', r'Grand Rapids', r'Green Bay', r'Greensboro', r'Gresham', r'Guangzhou', r'Hampton', r'Hartford', r'Hayward', r'Henderson', r'High Point', r'Hollywood', r'Honolulu', r'Huntington Beach', r'Huntsville', r'Independence', r'Indianapolis', r'Inglewood', r'Irvine', r'Irving', r'Jackson', r'Joliet', r'Kent', r'Killeen', r'Knoxville', r'Lafayette', r'Lakeland', r'Lakewood', r'Lancaster', r'Lansing', r'Laredo', r'Las Cruces', r'Las Vegas', r'Lewisville', r'Little Rock', r'Lowell', r'Macon', r'McAllen', r'McKinney', r'Mesquite', r'Miami Gardens', r'Midland', r'Milwaukee', r'Miramar', r'Mobile', r'Modesto', r'Montgomery', r'Moreno Valley', r'Murfreesboro', r'Murrieta', r'Naperville', r'New Haven', r'Newport News', r'Norfolk', r'Norman', r'North Charleston', r'North Las Vegas', r'Norwalk', r'Oakland', r'Oceanside', r'Odessa', r'Olathe', r'Ontario', r'Orange', r'Overland Park', r'Oxnard', r'Palm Bay', r'Palmdale', r'Pasadena', r'Paterson', r'Pearland', r'Pembroke Pines', r'Peoria', r'Pittsburgh', r'Plano', r'Pomona', r'Pompano Beach', r'Providence', r'Provo', r'Pueblo', r'Rancho Cucamonga', r'Reno', r'Rialto', r'Richardson', r'Riverside', r'Rochester', r'Rockford', r'Roseville', r'Round Rock', r'Sacramento', r'Saint Paul', r'Salem', r'Salinas', r'Salt Lake City', r'San Bernardino', r'San Jose', r'San Mateo', r'Sandy Springs', r'Santa Clara', r'Santa Clarita', r'Santa Maria', r'Santa Rosa', r'Savannah', r'Scottsdale', r'Shanghai', r'Shenyang', r'Shenzhen', r'Shreveport', r'Simi Valley', r'Sioux Falls', r'South Bend', r'Springfield', r'Stamford', r'Sterling Heights', r'Sunnyvale', r'Surprise', r'Suzhou', r'Syracuse', r'Tacoma', r'Tallahassee', r'Temecula', r'Tempe', r'Thornton', r'Thousand Oaks', r'Tianjing', r'Toledo', r'Topeka', r'Torrance', r'Tucson', r'Tulsa', r'Tyler', r'Vallejo', r'Ventura', r'Victorville', r'Visalia', r'Waco', r'Warren', r'Waterbury', r'West Covina', r'West Jordan', r'West Palm Beach', r'West Valley City', r'Westminster', r'Wichita', r'Wichita Falls', r'Wilmington', r'Winston-Salem', r'Worcester', r'Wuxi', r'Xiamen', r'Yonkers', r'Bentonville', r'Afghanistan', r'AK', r'AL', r'Alabama', r'Åland', r'Åland Islands', r'Alaska', r'Albania', r'Algeria', r'American Samoa', r'Andorra', r'Angola', r'Anguilla', r'Antarctica', r'Antigua and Barbuda', r'AR', r'Argentina', r'Arizona', r'Arkansas', r'Armenia', r'Aruba', r'Australia', r'Austria', r'AZ', r'Azerbaijan', r'Bahamas', r'Bahrain', r'Bangladesh', r'Barbados', r'Belarus', r'Belgium', r'Belize', r'Benin', r'Bermuda', r'Bhutan', r'Bolivia', r'Bonaire', r'Bosnia', r'Bosnia and Herzegovina', r'Botswana', r'Bouvet Island', r'Brazil', r'British Indian Ocean Territory', r'British Virgin Islands', r'Brunei', r'Bulgaria', r'Burkina Faso', r'Burundi', r'CA', r'Cabo Verde', r'California', r'Cambodia', r'Cameroon', r'Canada', r'Cayman Islands', r'Central African Republic', r'Chad', r'Chile', r'China', r'Christmas Island', r'CO', r'Cocos Islands', r'Colombia', r'Colorado', r'Comoros', r'Congo', r'Congo (DRC)', r'Connecticut', r'Cook Islands', r'Costa Rica', r'Côte d’Ivoire', r'Croatia', r'CT', r'Cuba', r'Curaçao', r'Cyprus', r'Czechia', r'DE', r'Delaware', r'Denmark', r'Djibouti', r'Ecuador', r'Egypt', r'El Salvador', r'Eritrea', r'Estonia', r'eSwatini', r'Ethiopia', r'Falkland Islands', r'Falklands', r'Faroe Islands', r'Fiji', r'Finland', r'FL', r'Florida', r'France', r'French Guiana', r'French Polynesia', r'French Southern Territories', r'FYROM', r'GA', r'Gabon', r'Gambia', r'Georgia', r'Georgia', r'Germany', r'Ghana', r'Gibraltar', r'Greece', r'Greenland', r'Grenada', r'Guadeloupe', r'Guam', r'Guatemala', r'Guernsey', r'Guyana', r'Haiti', r'Hawaii', r'Herzegovina', r'HI', r'Honduras', r'Hong Kong', r'Hungary', r'IA', r'Iceland', r'ID', r'Idaho', r'IL', r'Illinois', r'IN', r'India', r'Indiana', r'Indonesia', r'Iowa', r'Iran', r'Iraq', r'Ireland', r'Isle of Man', r'Israel', r'Italy', r'Ivory Coast', r'Jamaica', r'Jan Mayen', r'Japan', r'Jersey', r'Jordan', r'Kansas', r'Kazakhstan', r'Keeling Islands', r'Kentucky', r'Kenya', r'Kiribati', r'Korea', r'Kosovo', r'KS', r'Kuwait', r'KY', r'Kyrgyzstan', r'LA', r'Laos', r'Latvia', r'Lebanon', r'Lesotho', r'Liberia', r'Libya', r'Liechtenstein', r'Lithuania', r'Louisiana', r'Luxembourg', r'MA', r'Macao', r'Macedonia', r'Madagascar', r'Maine', r'Malawi', r'Malaysia', r'Maldives', r'Mali', r'Malta', r'Marshall Islands', r'Martinique', r'Maryland', r'Massachusetts', r'Mauritania', r'Mauritius', r'Mayotte', r'MD', r'ME', r'MI', r'Michigan', r'Micronesia', r'Minnesota', r'Mississippi', r'Missouri', r'MN', r'MO', r'Moldova', r'Monaco', r'Mongolia', r'Montana', r'Montenegro', r'Montserrat', r'Morocco', r'Mozambique', r'MS', r'MT', r'Myanmar', r'Namibia', r'Nauru', r'NC', r'ND', r'NE', r'Nebraska', r'Nepal', r'Netherlands', r'Nevada', r'New Caledonia', r'New Hampshire', r'New Jersey', r'New Zealand', r'NH', r'Nicaragua', r'Niger', r'Nigeria', r'Niue', r'NJ', r'NM', r'Norfolk Island', r'North Carolina', r'North Dakota', r'North Korea', r'Northern Mariana Islands', r'Norway', r'NV', r'NY', r'OH', r'Ohio', r'OK', r'Oklahoma', r'Oman', r'OR', r'Oregon', r'PA', r'Pakistan', r'Palau', r'Palestinian Authority', r'Panama', r'Paraguay', r'Pennsylvania', r'Peru', r'Philippines', r'Pitcairn Islands', r'Poland', r'Portugal', r'Puerto Rico', r'Qatar', r'Réunion', r'Rhode Island', r'RI', r'Romania', r'Russia', r'Rwanda', r'Saba', r'Saint Barthélemy', r'Saint Kitts and Nevis', r'Saint Lucia', r'Saint Martin', r'Saint Pierre and Miquelon', r'Saint Vincent and the Grenadines', r'Samoa', r'San Marino', r'São Tomé and Príncipe', r'Saudi Arabia', r'SC', r'SD', r'Senegal', r'Serbia', r'Seychelles', r'Sierra Leone', r'Singapore', r'Sint Eustatius', r'Sint Maarten', r'Slovakia', r'Slovenia', r'Solomon Islands', r'Somalia', r'South Africa', r'South Carolina', r'South Dakota', r'South Sudan', r'Spain', r'Sri Lanka', r'Sudan', r'Suriname', r'Svalbard', r'Swaziland', r'Sweden', r'Switzerland', r'Syria', r'Taiwan', r'Tajikistan', r'Tanzania', r'Tennessee', r'Texas', r'Thailand', r'Timor-Leste', r'TN', r'Togo', r'Tokelau', r'Tonga', r'Trinidad and Tobago', r'Tunisia', r'Turkey', r'Turkmenistan', r'Turks and Caicos Islands', r'Tuvalu', r'TX', r'U.S. Outlying Islands', r'US Outlying Islands', r'U.S. Virgin Islands', r'US Virgin Islands', r'Uganda', r'Ukraine', r'United Arab Emirates', r'United Kingdom', r'United States', r'Uruguay', r'UT', r'Utah', r'Uzbekistan', r'VA', r'Vanuatu', r'Vatican City', r'Venezuela', r'Vermont', r'Vietnam', r'Virginia', r'VT', r'WA', r'Wallis and Futuna', r'West Virginia', r'WI', r'Wisconsin', r'WV', r'WY', r'Wyoming', r'Yemen', r'Zambia', r'Zimbabwe', r'Paris', r'Tokyo', r'Shanghai', r'Sao Paulo', r'Rio de Janeiro', r'Rio', r'Brasília', r'Brasilia', r'Recife', r'Milan', r'Mumbai', r'Moscow', r'Frankfurt', r'Munich', r'Berlim', r'Madrid', r'Lisbon', r'Warsaw', r'Johannesburg', r'Seoul', r'Istanbul', r'Kuala Kumpur', r'Jakarta', r'Amsterdam', r'Brussels', r'Valencia', r'Seville', r'Bilbao', r'Malaga', r'Las Palmas', r'Zaragoza', r'Alicante', r'Elche', r'Oviedo', r'Gijón', r'Avilés', r'West Coast', r'Central', r'Pacific', r'Eastern', r'Mountain']
# pylint: enable=line-too-long
