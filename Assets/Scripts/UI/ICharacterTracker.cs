public interface ICharacterTracker<T> where T : CharacterManager
{
    void SetTracking(T tracked_character);
}
