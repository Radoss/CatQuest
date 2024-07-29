public interface IDamagable
{
    bool IsDead { get; set; }
    void TakeDamage(int damage);
}
