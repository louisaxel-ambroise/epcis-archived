using FasTnT.Domain.Services.Users;
using FasTnT.Domain.Services.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace FasTnT.Domain.Services.MasterdataCapture
{
    public class MasterdataCapturer : IMasterdataCapturer
    {
        private readonly IDocumentValidator _validator;
        private readonly IMasterdataParser _masterdataParser;
        private readonly IUserProvider _userProvider;

        public MasterdataCapturer(IDocumentValidator validator, IMasterdataParser masterdataParser, IUserProvider userProvider)
        {
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
            _masterdataParser = masterdataParser ?? throw new ArgumentNullException(nameof(masterdataParser));
            _userProvider = userProvider ?? throw new ArgumentNullException(nameof(userProvider));
        }

        public IEnumerable<string> Capture(XDocument document)
        {
            _validator.Validate(document);

            var masterData = _masterdataParser.Parse(document.Root);
            var currentUser = _userProvider.GetCurrentUser();

            // TODO: store

            return masterData.Select(x => x.Id);
        }
    }
}
