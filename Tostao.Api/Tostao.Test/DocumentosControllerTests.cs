using Microsoft.AspNetCore.Mvc;
using Moq;
using Tostao.Api.Controllers;
using Tostao.Application.DTOs;
using Tostao.Application.Interfaces;

namespace Tostao.Test
{
    [TestClass]
    public class DocumentosControllerTests
    {
        private readonly Mock<IDocumentoService> _mockService;
        private readonly DocumentosController _controller;

        public DocumentosControllerTests()
        {
            _mockService = new Mock<IDocumentoService>();
            _controller = new DocumentosController(_mockService.Object);
        }

        [TestMethod]
        public async Task CreateDocumento_ReturnsCreatedResult()
        {
            // Arrange
            var dto = new DocumentoCreateDto
            {
                Titulo = "Test Document",
                Autor = "Test Author",
                Tipo = "Informe"
            };
            var expected = new DocumentoReadDto(
                Guid.NewGuid(), "Test Document", "Test Author", "Informe",
                "Registrado", DateTime.UtcNow, null, null
            );

            _mockService.Setup(s => s.CreateAsync(It.IsAny<DocumentoCreateDto>()))
                       .ReturnsAsync(expected);

            // Act
            var result = await _controller.CreateDocumento(dto);

            // Assert
            var createdResult = result.Result as CreatedAtActionResult;
            Assert.IsNotNull(createdResult);
            var returnValue = createdResult.Value as DocumentoReadDto;
            Assert.IsNotNull(returnValue);
            Assert.AreEqual(expected.Titulo, returnValue.Titulo);
        }

        [TestMethod]
        public async Task GetDocumento_ReturnsOkResult()
        {
            // Arrange
            var id = Guid.NewGuid();
            var expected = new DocumentoReadDto(
                id, "Test Document", "Test Author", "Informe",
                "Registrado", DateTime.UtcNow, null, null
            );

            _mockService.Setup(s => s.GetByIdAsync(id))
                       .ReturnsAsync(expected);

            // Act
            var result = await _controller.GetDocumento(id);

            // Assert
            var okResult = result.Result as OkObjectResult;
            Assert.IsNotNull(okResult);
            var returnValue = okResult.Value as DocumentoReadDto;
            Assert.IsNotNull(returnValue);
            Assert.AreEqual(id, returnValue.Id);
        }

        [TestMethod]
        public async Task GetDocumento_ReturnsNotFound_WhenDocumentoNotExists()
        {
            // Arrange
            var id = Guid.NewGuid();
            _mockService.Setup(s => s.GetByIdAsync(id))
                       .ThrowsAsync(new KeyNotFoundException());

            // Act
            var result = await _controller.GetDocumento(id);

            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task GetDocumentos_ReturnsOkResult()
        {
            // Arrange
            var documentos = new List<DocumentoReadDto>
            {
                new DocumentoReadDto(Guid.NewGuid(), "Doc1", "Author1", "Informe", "Registrado", DateTime.UtcNow, null, null),
                new DocumentoReadDto(Guid.NewGuid(), "Doc2", "Author2", "Contrato", "Validado", DateTime.UtcNow, DateTime.UtcNow, null)
            };

            _mockService.Setup(s => s.GetAllAsync(1, 10))
                       .ReturnsAsync(documentos);

            // Act
            var result = await _controller.GetDocumentos();

            // Assert
            var okResult = result.Result as OkObjectResult;
            Assert.IsNotNull(okResult);
            var returnValue = okResult.Value as List<DocumentoReadDto>;
            Assert.IsNotNull(returnValue);
            Assert.AreEqual(2, returnValue.Count);
        }

        [TestMethod]
        public async Task DeleteDocumento_ReturnsNoContent_WhenSuccess()
        {
            // Arrange
            var id = Guid.NewGuid();
            _mockService.Setup(s => s.DeleteAsync(id))
                       .ReturnsAsync(true);

            // Act
            var result = await _controller.DeleteDocumento(id);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }

        [TestMethod]
        public async Task DeleteDocumento_ReturnsNotFound_WhenDocumentoNotExists()
        {
            // Arrange
            var id = Guid.NewGuid();
            _mockService.Setup(s => s.DeleteAsync(id))
                       .ReturnsAsync(false);

            // Act
            var result = await _controller.DeleteDocumento(id);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }
    }
}