using FasTnT.Domain.Repositories;
using FasTnT.Domain.Services.Users;
using FasTnT.Domain.Services.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace FasTnT.Domain.Services.Capture.Masterdata
{
    public class MasterdataCapturer : IMasterdataCapturer
    {
        private readonly IDocumentValidator _validator;
        private readonly IMasterdataParser _masterdataParser;
        private readonly IUserProvider _userProvider;
        private readonly IMasterdataRepository _masterdataRepository;

        public MasterdataCapturer(IDocumentValidator validator, IMasterdataParser masterdataParser, IUserProvider userProvider, IMasterdataRepository masterdataRepository)
        {
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
            _masterdataParser = masterdataParser ?? throw new ArgumentNullException(nameof(masterdataParser));
            _userProvider = userProvider ?? throw new ArgumentNullException(nameof(userProvider));
            _masterdataRepository = masterdataRepository ?? throw new ArgumentNullException(nameof(masterdataRepository));
        }

        public IEnumerable<string> Capture(XDocument document)
        {
            _validator.Validate(document);

            var masterdataList = _masterdataParser.Parse(document.Root);
            var currentUser = _userProvider.GetCurrentUser();

            foreach (var masterdata in masterdataList)
            {
                _masterdataRepository.Store(masterdata);
            }

            return masterdataList.Select(x => x.Id);
        }
    }
}
