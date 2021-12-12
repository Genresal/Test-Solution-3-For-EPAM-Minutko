using AutoMapper;
using EMR.Business.Models;
using EMR.Business.Services;
using EMR.Services;
using EMR.ViewModels;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace EMR.Web.Tests
{
    class TestRecordPageService
    {
        private Mock<IRecordService> _mockRecordService;
        private Mock<IDoctorService> _mockDoctorService;
        private Mock<IPatientService> _mockPatientService;
        private Mock<IPositionService> _mockPositionService;
        private Mock<IMapper> _mockMapper;
        private List<Record> _records;

        [SetUp]
        public void Setup()
        {
            _mockRecordService = new Mock<IRecordService>();
            _mockDoctorService = new Mock<IDoctorService>();
            _mockPatientService = new Mock<IPatientService>();
            _mockPositionService = new Mock<IPositionService>();
            _mockMapper = new Mock<IMapper>();

            _records = new List<Record>
            {
                new Record{ Id = 1},
                new Record{ Id = 2},
                new Record{ Id = 3},
                new Record{ Id = 4},
                new Record{ Id = 5},
                new Record{ Id = 6},
                new Record{ Id = 7},
                new Record{ Id = 8},
                new Record{ Id = 9}
            };
        }

        [Test]
        public void GetRecordsDetails_ByIdEqualsZero_ReturnsResultFromGetLastMethod()
        {
            int id = 0;
            var expected = new RecordDetailsViewModel() { Id = GetTestLastRecord().Id };

            _mockRecordService.Setup(s => s.GetLast()).Returns(GetTestLastRecord());
            _mockRecordService.Setup(s => s.GetById(1)).Returns(GetTestRecordById(1));
            _mockMapper.Setup(s => s.Map<Record, RecordDetailsViewModel>(It.Is<Record>(x => x.Id == GetTestLastRecord().Id))).Returns(expected);

            var service = new RecordPageService(_mockRecordService.Object,
                                                _mockDoctorService.Object,
                                                _mockPatientService.Object,
                                                _mockPositionService.Object,
                                                _mockMapper.Object);

            var result = service.Details(id);

            Assert.AreEqual(result.Id, expected.Id);
        }

        [Test]
        public void GetRecordsDetails_ByIdEqualsSomeNum_ReturnsResultFromGetByIdMethod()
        {
            int id = 1;
            var expected = new RecordDetailsViewModel() { Id = GetTestRecordById(id).Id };

            _mockRecordService.Setup(s => s.GetLast()).Returns(GetTestLastRecord());
            _mockRecordService.Setup(s => s.GetById(id)).Returns(GetTestRecordById(id));
            _mockMapper.Setup(s => s.Map<Record, RecordDetailsViewModel>(It.Is<Record>(x => x.Id == GetTestRecordById(id).Id))).Returns(expected);

            var service = new RecordPageService(_mockRecordService.Object,
                                                _mockDoctorService.Object,
                                                _mockPatientService.Object,
                                                _mockPositionService.Object,
                                                _mockMapper.Object);

            var result = service.Details(id);

            Assert.AreEqual(result.Id, expected.Id);
        }

        private Record GetTestLastRecord()
        {
            return _records.Last();
        }

        private Record GetTestRecordById(int id)
        {
            return _records.First(x => x.Id == id);
        }
    }
}
