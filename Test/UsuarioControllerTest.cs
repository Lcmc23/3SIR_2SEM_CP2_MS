using Microsoft.AspNetCore.Mvc;
using Moq;
using Newtonsoft.Json;
using web_app_domain;
using web_app_repository;
using web_performance.Controllers;
using Xunit;

namespace Test
{
	public class UsuarioControllerTest
	{
		private readonly Mock<IUsuarioRepository> _userRepositoryMock;
		private readonly UsuarioController _controller;

		public UsuarioControllerTest()
		{
			_userRepositoryMock = new Mock<IUsuarioRepository>();
			_controller = new UsuarioController(_userRepositoryMock.Object);
		}

		[Fact]
		public async Task Get_ListarUsuarioOk()
		{
			//arrange
			var usuarios = new List<Usuario>()
			{
				new Usuario()
				{
					email = "lucas.cabral@gmail.com",
					id = 1,
					nome = "Lucas Cabral"
				}

			};

			_userRepositoryMock.Setup(r => r.ListarUsuarios()).ReturnsAsync(usuarios);

			//Act
			var result = await _controller.GetUsuario();

			//Asserts
			Assert.IsType<OkObjectResult>(result);
			var okResult = result as OkObjectResult;
			Assert.Equal(JsonConvert.SerializeObject(usuarios), JsonConvert.SerializeObject(okResult.Value));

        }

		[Fact]
		public async Task Get_ListarRetornoNotFound()
		{
			_userRepositoryMock.Setup(u => u.ListarUsuarios()).ReturnsAsync((IEnumerable<Usuario>)null);

			var result = await _controller.GetUsuario();
			Assert.IsType<NotFoundResult>(result);
		}

		[Fact]
		public async Task Post_SalvarUsuario()
		{
			//Arrange
			var usuario = new Usuario
			{
				id = 1,
				email = "update@gmail.com",
				nome = "Update"

			};
			_userRepositoryMock.Setup(u => u.SalvarUsuario(It.IsAny<Usuario>())).Returns(Task.CompletedTask);

			//Act
			var result =await _controller.Post(usuario);

			//Asserts
			_userRepositoryMock.Verify(u => u.SalvarUsuario(It.IsAny<Usuario>()), Times.Once);
            Assert.IsType<OkObjectResult>(result);
        }

	}
}

