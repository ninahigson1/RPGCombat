using FluentAssertions;
using RGPCombat;

namespace CombatTests
{
    public class CharacterTests
    {
        [Fact]
        public void Should_Be_Created_Correctly()
        {
            var character = new Character();

            character.Health.Should().Be(1000);
            character.Level.Should().Be(1);
            character.IsAlive.Should().BeTrue();
        }

        //[Theory]
        //[InlineData(10, 990)]
        //[InlineData(50, 950)]
        //[InlineData(500, 500)]
        //public void Should_Be_Able_TO_Damage_Another_Character(int damage, int expectedHealth)
        //{
        //    var attacker = new Character();
        //    var victim = new Character();

        //    attacker.Attack(victim, damage);

        //    victim.Health.Should().Be(expectedHealth);
        //} 

        [Fact]
        public void Should_Be_Not_Be_Able_To_Damage_Itself()
        {
            var attacker = new Character();
                       
            var attack = () => attacker.Attack(attacker, 100);

            attack.Should().Throw<InvalidOperationException>();
        }   

        [Fact]
        public void Victim_Dies_If_Damage_Reduces_Health_To_Zero_Or_Below()
        {
            var attacker = new Character();
            var victim = new Character();
            victim.Health = 10;

            attacker.Attack(victim, 11);

            victim.IsAlive.Should().BeFalse();
        }      

        [Fact]
        public void Can_Only_Heal_Itself()
        {
            // arrange
            var healer = new Character();
            var anotherCharacter = new Character();
            
            var heal = () => healer.Heal(anotherCharacter, 10);

            heal.Should().Throw<InvalidOperationException>();
        }   

        [Fact]
        public void Dead_Characters_Cannot_Be_Healed()
        {
            // arrange
            var deadCharacter = new Character();
            deadCharacter.IsAlive = false;
            deadCharacter.Health = 0;
            
            deadCharacter.Heal(deadCharacter, 10);

            deadCharacter.Health.Should().Be(0);
            deadCharacter.IsAlive.Should().BeFalse();
        }        

        [Fact]
        public void Characters_Cannot_Be_Healed_Above_Max_Health()
        {
            // arrange
            var target = new Character();
            target.Health = 999;
            
            target.Heal(target, 10);

            target.Health.Should().Be(1000);
        }  

        [Theory]
        [InlineData(10, 510)]
        [InlineData(50, 550)]
        [InlineData(500, 1000)]
        public void Should_Be_Able_TO_Heal_Another_Character(int health, int expectedHealth)
        {
            // arrange
            var target = new Character();
            target.Health = 500;
            
            // act
            target.Heal(target, health);

            // assert
            target.Health.Should().Be(expectedHealth);
        }

        [Fact]
        public void Higher_Levels_Damage_Is_Reduced()
        {
            // arrange
            var attacker = new Character();
            var target = new Character();
            target.Level = 10;
                    
            // act
            attacker.Attack(target, 100);

            // assert
            target.Health.Should().Be(950); // 1000 - (100 / 2)
        }

        [Fact]
        public void Lower_Levels_Damage_Is_Increased()
        {
            // arrange
            var attacker = new Character();
            var target = new Character();
            target.Level = 2;
            attacker.Level = 10;

            // act
            attacker.Attack(target, 100);

            // assert
            target.Health.Should().Be(850); // 1000 - (100 * 1.5)
        }
    }
}