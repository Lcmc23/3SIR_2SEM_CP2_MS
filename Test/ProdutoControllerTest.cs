using Microsoft.AspNetCore.Mvc;
using Moq;
using Newtonsoft.Json;
using web_app_domain;
using web_app_repository;
using web_performance.Controllers;
using Xunit;
namespace Test
{
    public class ProdutoControllerTest
	{
        private readonly Mock<IProdutoRepository> _produtoRepositoryMock;
        private readonly ProdutoController _controller;

        public ProdutoControllerTest()
        {
            _produtoRepositoryMock = new Mock<IProdutoRepository>();
            _controller = new ProdutoController(_produtoRepositoryMock.Object);
        }

        [Fact]
        public async Task Get_ListarProdutoOk()
        {
            //Arrange
            var produtos = new List<Produto>()
            {
                new Produto()
                {
                    id = 1,
                    nome = "Macarrão",
                    preco = "3.34",
                    quantidade_estoque = "35",
                    data_criacao = "11/10/24"

                }
            };

            _produtoRepositoryMock.Setup(r => r.ListarProdutos()).ReturnsAsync(produtos);

            //Act
            var result = await _controller.GetProdutos();

            //Asserts
            Assert.IsType<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.Equal(JsonConvert.SerializeObject(produtos), JsonConvert.SerializeObject(okResult.Value));

        }

        [Fact]
        public async Task Get_ListarRetornoNotFound()
        {
            _produtoRepositoryMock.Setup(u => u.ListarProdutos()).ReturnsAsync((IEnumerable<Produto>)null);

            var result = await _controller.GetProdutos();
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Post_SalvarProduto()
        {
            //Arrange
            var produto = new Produto
            {
                id = 1,
                nome = "Update_Macarrão",
                preco = "4",
                quantidade_estoque = "40",
                data_criacao = "11/10/24"

            };
            _produtoRepositoryMock.Setup(u => u.SalvarProduto(It.IsAny<Produto>())).Returns(Task.CompletedTask);

            //Act
            var result = await _controller.Post(produto);

            //Asserts
            _produtoRepositoryMock.Verify(u => u.SalvarProduto(It.IsAny<Produto>()), Times.Once);
            Assert.IsType<OkObjectResult>(result);
        }
    }
}

