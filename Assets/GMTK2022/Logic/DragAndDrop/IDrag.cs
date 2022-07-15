//Inspired by https://www.youtube.com/watch?v=HfqRKy5oFDQ

public interface IDrag
{
    void OnStartDrag();
    void OnEndDrag();
    bool CanBeDragged();
}