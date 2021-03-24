namespace RecipesPlus.Services.Data.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Moq;
    using RecipesPlus.Data.Common.Repositories;
    using RecipesPlus.Data.Models;
    using Xunit;

    public class VotesServiceTests
    {
        [Fact]

        public async Task WhenUserVotesMoreThanOneTimeForOneRecipeOnlyTheLastShouldBeCounted()
        {
            var list = new List<Vote>();
            var mockRepo = new Mock<IRepository<Vote>>();
            mockRepo.Setup(x => x.All()).Returns(list.AsQueryable());
            mockRepo.Setup(x => x.AddAsync(It.IsAny<Vote>())).Callback((Vote vote) => list.Add(vote));

            var service = new VotesService(mockRepo.Object);

            await service.SetVoteAsync(1, "1", 1);
            await service.SetVoteAsync(1, "1", 5);
            await service.SetVoteAsync(1, "1", 4);
            await service.SetVoteAsync(1, "1", 3);
            await service.SetVoteAsync(1, "1", 2);

            Assert.Equal(1, list.Count());
            Assert.Equal(2, list.First().Value);
        }

        [Fact]

        public async Task WhenTwoUsersVoteForTheSameRecipeTheAverageVoteShouldBeCorrect()
        {
            var list = new List<Vote>();
            var mockRepo = new Mock<IRepository<Vote>>();
            mockRepo.Setup(x => x.All()).Returns(list.AsQueryable());
            mockRepo.Setup(x => x.AddAsync(It.IsAny<Vote>())).Callback((Vote vote) => list.Add(vote));

            var service = new VotesService(mockRepo.Object);

            await service.SetVoteAsync(1, "1", 4);
            await service.SetVoteAsync(1, "2", 5);

            // Check if the method AddAsync is called exactly 2 times
            mockRepo.Verify(x => x.AddAsync(It.IsAny<Vote>()), Times.Exactly(2));

            Assert.Equal(4.5, service.GetAverageVotes(1));

        }
    }

    // public class FakeVotesRepository : IRepository<Vote>
    // {
    //    private List<Vote> list = new List<Vote>();

    // public Task AddAsync(Vote entity)
    //    {
    //        this.list.Add(entity);
    //        return Task.CompletedTask;
    //    }

    // public IQueryable<Vote> All()
    //    {
    //        return this.list.AsQueryable();
    //    }

    // public IQueryable<Vote> AllAsNoTracking()
    //    {
    //        return this.list.AsQueryable();
    //    }

    // public void Delete(Vote entity)
    //    {
    //    }

    // public void Dispose()
    //    {
    //    }

    // public Task<int> SaveChangesAsync()
    //    {
    //        return Task.FromResult(0);
    //    }

    // public void Update(Vote entity)
    //    {
    //    }
    // }
}
