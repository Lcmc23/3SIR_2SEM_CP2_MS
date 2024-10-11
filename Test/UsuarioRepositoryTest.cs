using Moq;
using web_app_domain;
using web_app_repository;
using Xunit;

namespace Test
{
	public class UsuarioRepositoryTest
	{
		[Fact]
		public async Task ListarUsuarios()
		{
            //Arrange
            var usuarios = new List<Usuario>()
            {
                new Usuario()
                {
                    email = "lucas.cabral@gmail.com",
                    id = 1,
                    nome = "Lucas Cabral"
                },

                new Usuario()
                {
                    email = "hugo@gmail.com",
                    id = 2,
                    nome = "Hugo Ramoz"
                }

            };

            var userRepositoryMock = new Mock<IUsuarioRepository>();
            userRepositoryMock.Setup(u => u.ListarUsuarios()).ReturnsAsync(usuarios);
            var userRepository = userRepositoryMock.Object;

            //Act
            var result = await userRepository.ListarUsuarios();

            //Asserts
            Assert.Equal(usuarios, result);
            
        }

     
        [Fact]
        public async Task SalvarUsuario()
        {
            //Arrange
            var usuario = new Usuario
            {
                id = 1,
                email = "update@gmail.com",
                nome = "Update"

            };

            var userRepositoryMock = new Mock<IUsuarioRepository>();
            userRepositoryMock.Setup(u => u.SalvarUsuario(It.IsAny<Usuario>())).Returns(Task.CompletedTask);
            var userRepository = userRepositoryMock.Object;

            //Act
            await userRepository.SalvarUsuario(usuario);

            //Assets
            userRepositoryMock.Verify(u => u.SalvarUsuario(It.IsAny<Usuario>()), Times.Once);

        }

	}
}

