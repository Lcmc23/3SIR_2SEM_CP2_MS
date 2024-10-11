using Microsoft.AspNetCore.Mvc;
using Moq;
using web_app_domain;
using web_app_repository;
using Xunit;
namespace Test
{
	public class ProdutoRepositoryTest
	{
        [Fact]
        public async Task ListarProdutos()
        {
            //Arrange
            var produtos = new List<Produto>()
            {
                new Produto()
                {
                    id = 2,
                    nome = "Banana",
                    preco = "7",
                    quantidade_estoque = "12345",
                    data_criacao = "11/10/24"

                },

                new Produto()
                {
                    id = 3,
                    nome = "Ovo",
                    preco = "12",
                    quantidade_estoque = "55",
                    data_criacao = "11/10/24"

                }

            };

            var produtoRepositoryMock = new Mock<IProdutoRepository>();
            produtoRepositoryMock.Setup(u => u.ListarProdutos()).ReturnsAsync(produtos);
            var produtoRepository = produtoRepositoryMock.Object;

            //Act
            var result = await produtoRepository.ListarProdutos();

            //Asserts
            Assert.Equal(produtos, result);

        }

        [Fact]
        public async Task SalvarProduto()
        {
            //Arrange
            var produto = new Produto
            {
                id = 2,
                nome = "UpdateBanana",
                preco = "7",
                quantidade_estoque = "54321",
                data_criacao = "11/10/24"

            };

            var produtoRepositoryMock = new Mock<IProdutoRepository>();
            produtoRepositoryMock.Setup(u => u.SalvarProduto(It.IsAny<Produto>())).Returns(Task.CompletedTask);
            var produtoRepository = produtoRepositoryMock.Object;

            //Act
            await produtoRepository.SalvarProduto(produto);

            //Assets
            produtoRepositoryMock.Verify(u => u.SalvarProduto(It.IsAny<Produto>()), Times.Once);

        }
    }
}

